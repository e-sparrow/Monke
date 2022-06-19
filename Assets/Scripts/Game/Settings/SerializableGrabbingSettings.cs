using System;
using Game.Grabbing;
using Game.Settings.Interfaces;
using UnityEngine;

namespace Game.Settings
{
    [Serializable]
    public class SerializableGrabbingSettings : IGrabbingSettings
    {
        [field: SerializeField]
        public CharacterModel CharacterModel
        {
            get;
            private set;
        }

        [field: SerializeField]
        public CharacterView CharacterView
        {
            get;
            private set;
        }

        [field: SerializeField]
        public LineRenderer LineRenderer
        {
            get;
            private set;
        }
    }
}