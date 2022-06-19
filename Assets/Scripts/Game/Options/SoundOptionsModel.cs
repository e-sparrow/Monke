using Game.Options.Interfaces;
using UnityEngine;

namespace Game.Options
{
    public class SoundOptionsModel : ISoundOptionsModel
    {
        public SoundOptionsModel(AudioSource audioSource)
        {
            _audioSource = audioSource;
        }
        
        private readonly AudioSource _audioSource;
        
        public void SetVolume(float volume)
        {
            _audioSource.volume = volume;
        }
    }
}