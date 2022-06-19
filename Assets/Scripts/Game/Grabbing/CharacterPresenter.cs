using System;
using ESparrow.Utils.Extensions;
using ESparrow.Utils.Helpers;
using ESparrow.Utils.Tools.Easing;
using ESparrow.Utils.Tools.Executing;
using ESparrow.Utils.Tools.Executing.Interfaces;
using Game.Camera.Enums;
using Game.Camera.Interfaces;
using Game.Constants;
using Game.Counting;
using Game.Counting.Interfaces;
using Game.Counting.Interfaces.Enums;
using Game.Grabbing.Interfaces;
using Game.Interactions.Enums;
using Game.Interactions.Interfaces;
using Game.Settings.Interfaces;
using Input;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Grabbing
{
    public class CharacterPresenter : IDisposable
    {
        public CharacterPresenter
        (
            IGrabbingSettings grabbingSettings,
            IJointSystem jointSystem, 
            IGameplaySettings gameplaySettings,
            IVisualSettings visualSettings,
            IAudioSettings audioSettings,
            ICameraTarget cameraTarget,
            ICounterContainer counterContainer,
            MemoryPool<AudioSource> audioSourcePool
        )
        {
            _grabbingSettings = grabbingSettings;
            _jointSystem = jointSystem;
            _gameplaySettings = gameplaySettings;
            _visualSettings = visualSettings;
            _audioSettings = audioSettings;
            _cameraTarget = cameraTarget;
            _counterContainer = counterContainer;
            _audioSourcePool = audioSourcePool;
            _runQueue = CreateRunQueue();
        }

        private readonly IGrabbingSettings _grabbingSettings;
        private readonly IJointSystem _jointSystem;
        private readonly IGameplaySettings _gameplaySettings;
        private readonly IVisualSettings _visualSettings;
        private readonly IAudioSettings _audioSettings;
        private readonly ICameraTarget _cameraTarget;
        private readonly ICounterContainer _counterContainer;
        private readonly IMemoryPool<AudioSource> _audioSourcePool;
        private readonly IRunQueue _runQueue;

        private readonly ICounter _speedCounter = new Counter();
        private readonly ICounter _maxSpeedCounter = new Counter();
        private readonly ICounter _wallHitCounter = new Counter();
        private readonly ICounter _jointHitCounter = new Counter();
        private readonly ICounter _jointGrabCounter = new Counter();

        private readonly Controls _controls = new Controls();

        private Vector3 _tempCharacterPosition;
        private Vector3 _force;
        private bool _isGrabbed;

        [Inject]
        private void Initialize()
        {
            _controls.Enable();
            
            HookControls();
            
            _grabbingSettings.CharacterView.OnHit += Hit;
            _cameraTarget.OnTargetChanged += CheckCameraTarget;
            
            _runQueue.Next();

            _counterContainer.AddCounter(_speedCounter, ECountType.Speed);
            _counterContainer.AddCounter(_maxSpeedCounter, ECountType.MaxSpeed);
            _counterContainer.AddCounter(_wallHitCounter, ECountType.WallHit);
            _counterContainer.AddCounter(_jointHitCounter, ECountType.JointHit);
            _counterContainer.AddCounter(_jointGrabCounter, ECountType.JointGrab);

            _tempCharacterPosition = _grabbingSettings.CharacterModel.Position;
            
            ProjectContext.Instance.FixedUpdateAsObservable().Subscribe(CheckSpeed);
            ProjectContext.Instance.FixedUpdateAsObservable().Subscribe(UseForce);
        }

        public void Dispose()
        {
            UnhookControls();
            
            _grabbingSettings.CharacterView.OnHit -= Hit;
            _cameraTarget.OnTargetChanged -= CheckCameraTarget;
            
            _controls.Disable();
            
            _counterContainer.RemoveCounter(ECountType.Speed);
            _counterContainer.RemoveCounter(ECountType.MaxSpeed);
            _counterContainer.RemoveCounter(ECountType.WallHit);
            _counterContainer.RemoveCounter(ECountType.JointHit);
            _counterContainer.RemoveCounter(ECountType.JointGrab);
        }

        private IRunQueue CreateRunQueue()
        {
            IJoint joint = null;
            
            var source = new Action[]
            {
                () => Grab(_jointSystem.FindNearestJoint(_grabbingSettings.CharacterModel.Position))
            };

            var queue = new RunQueue(source, Create);
            return queue;

            async void TryGrab()
            {
                var target = _jointSystem.FindNearestJoint(_grabbingSettings.CharacterModel.Position);

                var range = (target.Position - _grabbingSettings.CharacterModel.Position).magnitude;
                if (range <= _gameplaySettings.MaxGrabDistance)
                {
                    var duration = _visualSettings.GrabAnimationDuration;
                    var ease = new Ease(_visualSettings.RopeThrowingCurve);
                    
                    var delta = _grabbingSettings.CharacterModel.Position - target.Position;
                    var progress = delta.magnitude / _gameplaySettings.MaxGrabDistance;
                    var thickness = _visualSettings.RopeThicknessMultiplierCurve.Evaluate(progress);
                    
                    _grabbingSettings.LineRenderer.enabled = true;
                    _grabbingSettings.LineRenderer.colorGradient = _visualSettings.RopeColor;
                    _grabbingSettings.LineRenderer.widthCurve = _visualSettings.RopeThicknessCurve;
                    _grabbingSettings.LineRenderer.widthMultiplier = thickness;
                    _grabbingSettings.LineRenderer.SetPosition(1, _grabbingSettings.CharacterModel.Position);
                    
                    _grabbingSettings.CharacterModel.Grab(joint);
                    
                    await CoroutinesHelper.Graduate(SetProgress, duration, ease).StartAsync();
                    
                    Grab(target);

                    void SetProgress(float progress)
                    {
                        var position = Vector3.Lerp(_grabbingSettings.CharacterModel.Position, target.Position, progress);
                        _grabbingSettings.LineRenderer.SetPosition(1, position);
                    }
                }
            }

            void Grab(IJoint target)
            {
                joint = target;
                
                _grabbingSettings.CharacterModel.Grab(joint);

                _grabbingSettings.LineRenderer.enabled = true;
                _grabbingSettings.LineRenderer.colorGradient = _visualSettings.RopeColor;
                _grabbingSettings.LineRenderer.widthCurve = _visualSettings.RopeThicknessCurve;
                _grabbingSettings.LineRenderer.SetPosition(1, joint.Position);

                ProjectContext.Instance.gameObject.UpdateAsObservable().Subscribe(UpdatePosition);

                _jointGrabCounter.Value++;
                
                _isGrabbed = true;

                void UpdatePosition(Unit unit)
                {
                    var delta = _grabbingSettings.CharacterModel.Position - joint.Position;
                    var progress = delta.magnitude / _gameplaySettings.MaxGrabDistance;
                    var thickness = _visualSettings.RopeThicknessMultiplierCurve.Evaluate(progress);

                    _grabbingSettings.LineRenderer.widthMultiplier = thickness;
                    _grabbingSettings.LineRenderer.SetPosition(0, _grabbingSettings.CharacterModel.Position);
                    
                    _cameraTarget.SetPosition(ECameraTargetType.Game, joint.Position + (delta) / 2);
                }
            }

            async void Release()
            {
                _grabbingSettings.CharacterModel.Release();
                
                _isGrabbed = false;
                
                var duration = _visualSettings.ReleaseAnimationDuration;
                var ease = new Ease(_visualSettings.RopeThrowingCurve); 
                
                await CoroutinesHelper.Graduate(SetProgress, duration, ease).StartAsync();
                
                _grabbingSettings.LineRenderer.enabled = false;

                void SetProgress(float progress)
                {
                    var position = Vector3.Lerp(joint.Position, _grabbingSettings.CharacterModel.Position, progress);
                    _grabbingSettings.LineRenderer.SetPosition(1, position);
                }
            }

            Action Create()
            {
                return Do;

                void Do()
                {
                    if (_isGrabbed)
                    {
                        Release();
                    }
                    else
                    {
                        TryGrab();
                    }
                }
            }
        }

        private void Press(InputAction.CallbackContext context)
        {
            _runQueue.Next();
        }
        
        private void Orient(InputAction.CallbackContext context)
        {
            var orientation = context.ReadValue<Vector2>();
            Orient(orientation);
        }

        private void Orient(Vector2 orientation)
        {
            _force = orientation;
        }
        
        private void Tilt(InputAction.CallbackContext context)
        {
            var vector = context.ReadValue<Vector3>();
            
            var orientation = vector.ToVector2();
            orientation *= _gameplaySettings.TiltSensitivity;
            
            Orient(orientation);
        }

        private void CheckSpeed(Unit unit)
        {
            var position = _grabbingSettings.CharacterModel.Position;
            
            var delta = position - _tempCharacterPosition;
            _tempCharacterPosition = position;

            var speed = (int) (delta.magnitude * 1000);
            
            _speedCounter.Value = speed;

            if (speed > _maxSpeedCounter.Value)
            {
                _maxSpeedCounter.Value = speed;
            }
        }

        private void UseForce(Unit unit)
        {
            if (!_isGrabbed) return;
            
            var realForce = _force * Time.fixedDeltaTime * _gameplaySettings.TiltSensitivity;
            _grabbingSettings.CharacterModel.Rigidbody.AddForce(realForce);
        }

        private void HookControls()
        {
            _controls.Main.Press.performed += Press;
            _controls.Main.Orientation.performed += Orient;
            _controls.Main.Tilt.performed += Tilt;
        }

        private void UnhookControls()
        {
            _controls.Main.Press.performed -= Press;
            _controls.Main.Orientation.performed -= Orient;
            _controls.Main.Tilt.performed -= Tilt;
        }

        private void CheckCameraTarget(ECameraTargetType type)
        {
            if (type == ECameraTargetType.Game)
            {
                HookControls();
            }
            else
            {
                UnhookControls();
            }
        }

        private void Hit(IInteractor interactor)
        {
            var audioSource = _audioSourcePool.Spawn();
            
            var clip = _audioSettings.AudioStorage[AudioConstants.Hit];

            var volume = _grabbingSettings.CharacterModel.Rigidbody.velocity.magnitude / 10;
            audioSource.PlayOneShot(clip, volume);
            
            _audioSourcePool.Despawn(audioSource);

            switch (interactor.Type)
            {
                case EInteractorType.Wall:
                    _wallHitCounter.Value++;
                    break;
                case EInteractorType.Joint:
                    _jointHitCounter.Value++;
                    break;
            }
        }
    }
}