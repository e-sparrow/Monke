using Game.Camera.Enums;
using Game.Camera.Interfaces;
using Game.UI.Interfaces;
using UnityEngine;

namespace Game.UI
{
    public class CameraTargetClickablePanelModel : IClickablePanelModel
    {
        public CameraTargetClickablePanelModel(ECameraTargetType targetType, Vector3 position, ICameraTarget cameraTarget)
        {
            _targetType = targetType;
            _position = position;
            _cameraTarget = cameraTarget;
        }

        private readonly ECameraTargetType _targetType;
        private readonly Vector3 _position;
        private readonly ICameraTarget _cameraTarget;
        
        public void Click()
        {
            _cameraTarget.SetPosition(_targetType, _position);
            _cameraTarget.SetTarget(_targetType);
        }
    }
}