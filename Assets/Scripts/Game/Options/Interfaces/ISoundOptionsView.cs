using System;

namespace Game.Options.Interfaces
{
    public interface ISoundOptionsView
    {
        event Action<float> OnVolumeChanged;
    }
}