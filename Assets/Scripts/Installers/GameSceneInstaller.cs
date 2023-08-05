using Cam;
using UnityEngine;
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
        //camera
        Container.Bind<CameraEffectsController>().FromComponentInNewPrefab(mainCamera).AsSingle();
        
    }
}