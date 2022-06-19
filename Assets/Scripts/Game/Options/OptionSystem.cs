using Game.Options.Interfaces;
using Game.Settings.Interfaces;

namespace Game.Options
{
    public class OptionSystem : IOptionSystem
    {
        public OptionSystem(ISceneSettings sceneSettings)
        {
            _sceneSettings = sceneSettings;
        }

        private readonly ISceneSettings _sceneSettings;
        
        private ISoundOptionsView _musicOptionsView;
        
        public void SetMusicOptionView(ISoundOptionsView view)
        {
            _musicOptionsView = view;

            var model = new SoundOptionsModel(_sceneSettings.AmbientSource);
            
            var presenter = new SoundOptionsPresenter(view, model);
            presenter.Initialize();
        }
    }
}