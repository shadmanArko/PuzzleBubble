using Bubble;
using Cam;
using Effects;
using Managers;
using ShootEffect;
using Signals;
using UnityEngine;
using Utils;
using Zenject;

[CreateAssetMenu(fileName = "GameSceneInstaller", menuName = "Installers/GameSceneInstaller")]
public class GameSceneInstaller : ScriptableObjectInstaller<GameSceneInstaller>
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject mouseShootView;
    [SerializeField] private GameObject dynamicEnvironment;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject scoreUiPrefab;
    [SerializeField] private GameObject gameOverUiPrefab;
    public override void InstallBindings()
    {
        // camera
        Container.Bind<CameraEffectsController>().FromComponentInNewPrefab(mainCamera).AsSingle();
        
        // signals
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<GameStateChangeSignal>();
        Container.DeclareSignal<BubbleCollisionSignal>();
        Container.DeclareSignal<CeilingCollisionSignal>();
        Container.DeclareSignal<StrikerFinishedSignal>();
        Container.DeclareSignal<StrikeSignal>();
        Container.DeclareSignal<ScoreUpdateSignal>();
        
        //Game States
        Container.Bind<GameStateController>().AsSingle();
        
        //Data Container
        Container.Bind<BubbleDataContainer>().FromScriptableObjectResource(Constants.BubbleDataContainerPath).AsSingle().NonLazy();
        
        // Explosion Factory
        Container.BindFactory<Vector3, ExplosionController, ExplosionController.Factory>().FromPoolableMemoryPool(
            x => x.WithInitialSize(10).FromComponentInNewPrefab(explosionPrefab).UnderTransformGroup("Explosions")); //todo animation
        
        // Bubble Factory
        Container.BindFactory<string, IBubbleNodeController, BubbleFactory>()
            .FromFactory<BubbleNodeFactory>();
        
        // Striker Bubble Factory
        Container.BindFactory<string, Vector2, StrikerController, StrikerController.Factory>();
        
        // Bubble Graph
        Container.Bind<ColorMergeHelper>().AsSingle();
        Container.Bind<NodeIsolationHelper>().AsSingle();
        Container.Bind<NumericMergeHelper>().AsSingle();
        Container.Bind<BubbleAttachmentHelper>().AsSingle();
        Container.Bind<BubbleGraph>().AsSingle();
        
        // // Environment
        // Container.Bind<DynamicEnvironmentView>().FromComponentInNewPrefab(_dynamicEnvironment).AsSingle();
        // Container.BindInterfacesAndSelfTo<DynamicEnvironmentController>().AsSingle();
        
        //UI todo
        
        // Mouse Input
        Container.Bind<MouseShootView>().FromComponentInNewPrefab(mouseShootView).AsSingle();
        Container.BindInterfacesAndSelfTo<MouseShootController>().AsSingle().NonLazy();
        
        // Managers
        Container.BindInterfacesAndSelfTo<StrikerManager>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GameSceneManager>().AsSingle();

    }
}