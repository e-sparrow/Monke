using System;
using System.Collections.Generic;
using Game.Camera.Enums;
using Game.Camera.Interfaces;
using Game.Settings.Interfaces;
using UnityEngine;

namespace Game.Camera
{
    public class CameraTarget : ICameraTarget
    {
        public CameraTarget(ISceneSettings sceneSettings)
        {
            _sceneSettings = sceneSettings;
        }

        public event Action<ECameraTargetType> OnTargetChanged = _ => { };

        private readonly ISceneSettings _sceneSettings;

        private readonly IDictionary<ECameraTargetType, Vector3> _positions 
            = new Dictionary<ECameraTargetType, Vector3>();

        private ECameraTargetType _type = ECameraTargetType.Game;

        public void SetTarget(ECameraTargetType type)
        {
            if (_type != type)
            {
                OnTargetChanged.Invoke(type);
            }

            _type = type;
            _sceneSettings.CameraTarget.transform.position = _positions[type];
        }

        public void SetPosition(ECameraTargetType type, Vector3 position)
        {
            _positions[type] = position;
            
            if (_type == type)
            {
                _sceneSettings.CameraTarget.transform.position = position;
            }
        }
    }
}