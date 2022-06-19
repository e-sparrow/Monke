using System;
using Game.Settings.Interfaces;
using UnityEngine;

namespace Game.Settings
{
    [Serializable]
    public class SerializableGameplaySettings : IGameplaySettings
    {
        [field: SerializeField]
        public float TiltSensitivity
        {
            get;
            private set;
        }

        [field: SerializeField]
        public float MaxGrabDistance
        {
            get;
            private set;
        }
    }
}