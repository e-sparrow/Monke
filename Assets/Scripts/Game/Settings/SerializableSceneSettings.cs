using System;
using Game.Settings.Interfaces;
using UnityEngine;

namespace Game.Settings
{
    [Serializable]
    public class SerializableSceneSettings : ISceneSettings
    {
        [field: SerializeField]
        public Transform CameraTarget
        {
            get;
            private set;
        }

        [field: SerializeField]
        public AudioSource AmbientSource
        {
            get;
            private set;
        }
    }
}