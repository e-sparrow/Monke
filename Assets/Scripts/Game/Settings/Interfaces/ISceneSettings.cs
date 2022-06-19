using UnityEngine;

namespace Game.Settings.Interfaces
{
    public interface ISceneSettings
    {
        Transform CameraTarget
        {
            get;
        }

        AudioSource AmbientSource
        {
            get;
        }
    }
}