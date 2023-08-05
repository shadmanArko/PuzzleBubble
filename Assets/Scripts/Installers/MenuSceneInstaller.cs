using MenuScene;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = "MenuSceneInstaller", menuName = "Installers/MenuSceneInstaller")]
    public class MenuSceneInstaller : ScriptableObjectInstaller<MenuSceneInstaller>
    {
        [SerializeField] private MenuSceneUiView menuSceneUiViewPrefab;
        public override void InstallBindings()
        {
            Container.Bind<MenuSceneUiView>().FromComponentInNewPrefab(menuSceneUiViewPrefab).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MenuSceneUiController>().AsSingle().NonLazy();
        }
    }
}