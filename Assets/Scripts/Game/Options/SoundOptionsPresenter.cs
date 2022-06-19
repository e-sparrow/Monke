using System;
using Game.Options.Interfaces;
using Zenject;

namespace Game.Options
{
    public class SoundOptionsPresenter : IInitializable, IDisposable
    {
        public SoundOptionsPresenter(ISoundOptionsView view, ISoundOptionsModel model)
        {
            _view = view;
            _model = model;
        }

        private readonly ISoundOptionsView _view;
        private readonly ISoundOptionsModel _model;
        
        public void Initialize()
        {
            _view.OnVolumeChanged += _model.SetVolume;
        }

        public void Dispose()
        {
            _view.OnVolumeChanged -= _model.SetVolume;
        }
    }
}