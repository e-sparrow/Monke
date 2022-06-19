using System.Collections.Generic;
using UnityEngine;

namespace Game.Settings.Interfaces
{
    public interface IAudioSettings
    {
        AudioSource AudioSourcePrefab
        {
            get;
        }

        int MinAudioPoolSize
        {
            get;
        }

        int MaxAudioPoolSize
        {
            get;
        }

        IDictionary<string, AudioClip> AudioStorage
        {
            get;
        }
    }
}