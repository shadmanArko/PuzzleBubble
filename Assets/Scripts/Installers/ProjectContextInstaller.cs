using Audio;
using LevelGeneration;
using UnityEngine;
using Utils;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = "ProjectContextInstaller", menuName = "Installers/ProjectContextInstaller")]
    public class ProjectContextInstaller : ScriptableObjectInstaller<ProjectContextInstaller>
    {
        [SerializeField] private GameObject audioController;
        public override void InstallBindings()
        {
            Container.Bind<AudioController>().FromComponentInNewPrefab(audioController).AsSingle().NonLazy();
            Container.Bind<LevelDataContainer>().FromScriptableObjectResource(Constants.LevelDataContainerPath)
                .AsSingle().NonLazy();
            Container.Bind<LevelDataContext>().AsSingle().NonLazy();
        }
    }
}