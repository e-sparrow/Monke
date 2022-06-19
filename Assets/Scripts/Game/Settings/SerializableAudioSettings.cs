using System;
using System.Collections.Generic;
using System.Linq;
using ESparrow.Utils.Extensions;
using ESparrow.Utils.Generic.Pairs;
using ESparrow.Utils.Generic.Pairs.Interfaces;
using Game.Settings.Interfaces;
using UnityEngine;

namespace Game.Settings
{
    [Serializable]
    public class SerializableAudioSettings : IAudioSettings
    {
        [SerializeField] private List<SerializablePair<string, AudioClip>> audioStorage;

        [field: SerializeField]
        public AudioSource AudioSourcePrefab
        {
            get;
            private set;
        }

        [field: SerializeField]
        public int MinAudioPoolSize
        {
            get;
            private set;
        }

        [field: SerializeField]
        public int MaxAudioPoolSize
        {
            get;
            private set;
        }

        public IDictionary<string, AudioClip> AudioStorage => audioStorage
            .SelectBase<SerializablePair<string, AudioClip>, IPair<string, AudioClip>>()
            .AsDictionary();
    }
}