using Game.Constants;
using Game.Settings;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    [CreateAssetMenu(menuName = "Game/Installers/Settings", fileName = "SettingsInstaller")]
    public class ProjectInstaller : ScriptableObjectInstaller<ProjectInstaller>
    {
        [SerializeField] private SerializableGameplaySettings gameplaySettings;
        [SerializeField] private SerializableVisualSettings visualSettings;
        [SerializeField] private SerializableAudioSettings audioSettings;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<SerializableGameplaySettings>()
                .FromInstance(gameplaySettings)
                .AsSingle();

            Container
                .BindInterfacesTo<SerializableVisualSettings>()
                .FromInstance(visualSettings)
                .AsSingle();
            
            Container
                .BindInterfacesTo<SerializableAudioSettings>()
                .FromInstance(audioSettings)
                .AsSingle();

            Container
                .BindMemoryPool<AudioSource, MemoryPool<AudioSource>>()
                .WithInitialSize(8)
                .WithMaxSize(16)
                .FromComponentInNewPrefab(audioSettings.AudioSourcePrefab)
                .UnderTransformGroup(ProjectConstants.AudioPoolTransformGroup);
        }
    }
}