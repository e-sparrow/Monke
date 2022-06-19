using System;
using DG.Tweening;
using Game.Options.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Options
{
    public class MonoSoundOptionsView : MonoBehaviour, ISoundOptionsView
    {
        public event Action<float> OnVolumeChanged = _ => { };

        [Header("Shake settings")]
        [SerializeField] private float duration = 0.5f;
        [SerializeField] private Vector2 strength = Vector2.one;
        [SerializeField] private int vibrato = 20;
        [Range(0f, 180f)]
        [SerializeField] private float randomness = 90f;
        
        [Space]

        [Header("References")]
        [SerializeField] private Slider slider;

        private IOptionSystem _optionSystem;

        [Inject]
        private void Construct(IOptionSystem optionSystem)
        {
            _optionSystem = optionSystem;
        }

        private void Start()
        {
            _optionSystem.SetMusicOptionView(this);
        }
        
        private void OnEnable()
        {
            slider.onValueChanged.AddListener(ChangeValue);
        }

        private void OnDisable()
        {
            slider.onValueChanged.RemoveListener(ChangeValue);
        }

        private void ChangeValue(float value)
        {
            OnVolumeChanged.Invoke(value);

            if (value == 0f)
            {
                transform
                    .DOShakePosition(duration, strength, vibrato, randomness);
            }
        }
    }
}