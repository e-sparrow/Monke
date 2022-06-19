using System;
using Game.Camera.Enums;
using UnityEngine;

namespace Game.Camera.Interfaces
{
    public interface ICameraTarget
    {
        event Action<ECameraTargetType> OnTargetChanged; 

        void SetTarget(ECameraTargetType type);
        void SetPosition(ECameraTargetType type, Vector3 position);
    }
}