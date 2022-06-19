using UnityEngine;

namespace Game.Settings.Interfaces
{
    public interface IVisualSettings
    {
        Gradient RopeColor
        {
            get;
        }

        AnimationCurve RopeThicknessCurve
        {
            get;
        }

        AnimationCurve RopeThicknessMultiplierCurve
        {
            get;
        }

        AnimationCurve RopeThrowingCurve
        {
            get;
        }
        
        float GrabAnimationDuration
        {
            get;
        }

        float ReleaseAnimationDuration
        {
            get;
        }
    }
}