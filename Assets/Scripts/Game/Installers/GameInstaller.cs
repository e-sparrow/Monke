using Game.Calibration;
using Game.Camera;
using Game.Coins;
using Game.Counting;
using Game.Grabbing;
using Game.Options;
using Game.Settings;
using Game.UI;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        [SerializeField] private SerializableGrabbingSettings grabbingSettings;
        [SerializeField] private SerializableCoinsSettings coinsSettings;
        [SerializeField] private SerializableSceneSettings sceneSettings;
        [SerializeField] private SerializableUISettings uiSettings;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<SerializableGrabbingSettings>()
                .FromInstance(grabbingSettings)
                .AsSingle();

            Container
                .BindInterfacesTo<SerializableCoinsSettings>()
                .FromInstance(coinsSettings)
                .AsSingle();

            Container
                .BindInterfacesTo<SerializableSceneSettings>()
                .FromInstance(sceneSettings)
                .AsSingle();

            Container
                .BindInterfacesTo<SerializableUISettings>()
                .FromInstance(uiSettings)
                .AsSingle();

            Container
                .BindInterfacesTo<OptionSystem>()
                .AsSingle();

            Container
                .BindInterfacesTo<CameraTarget>()
                .AsSingle();

            Container
                .BindInterfacesTo<Wallet>()
                .AsSingle();

            Container
                .BindInterfacesTo<JointSystem>()
                .AsSingle();

            Container
                .BindInterfacesTo<CounterContainer>()
                .AsSingle();

            Container
                .BindInterfacesTo<ClickablePanelSystem>()
                .AsSingle();

            Container
                .Bind<AccelerometerCalibrator>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesTo<CharacterPresenter>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<CoinSystem>()
                .AsSingle()
                .NonLazy();
        }
    }
}