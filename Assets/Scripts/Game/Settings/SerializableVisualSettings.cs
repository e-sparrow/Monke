using System;
using Game.Settings.Interfaces;
using UnityEngine;

namespace Game.Settings
{
    [Serializable]
    public class SerializableVisualSettings : IVisualSettings
    {
        [field: SerializeField]
        public Gradient RopeColor
        {
            get;
            private set;
        }
        
        [field: SerializeField]
        public AnimationCurve RopeThicknessCurve
        {
            get;
            private set;
        }

        [field: SerializeField]
        public AnimationCurve RopeThicknessMultiplierCurve
        {
            get;
            private set;
        }

        [field: SerializeField]
        public AnimationCurve RopeThrowingCurve
        {
            get;
            private set;
        }

        [field: SerializeField]
        public float GrabAnimationDuration
        {
            get;
            private set;
        }

        [field: SerializeField]
        public float ReleaseAnimationDuration
        {
            get;
            private set;
        }
    }
}