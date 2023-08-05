using Audio;
using LevelGeneration;
using UniRx;
using UnityEngine.SceneManagement;
using Utils;

namespace MenuScene
{
    public class MenuSceneUiController
    {
        private readonly MenuSceneUiView _menuSceneUiView;
        private readonly AudioController _audioController;

        public MenuSceneUiController(MenuSceneUiView menuSceneUiView, LevelDataContext levelDataContext,
            AudioController audioController)
        {
            _menuSceneUiView = menuSceneUiView;
            _audioController = audioController;

            for (int i = 0; i < _menuSceneUiView.LevelButtons.Length; i++)
            {
                var index = i;
                _menuSceneUiView.LevelButtons[i]
                    .OnClickAsObservable()
                    .Subscribe(_ =>
                    {
                        _audioController.ButtonClick();
                        levelDataContext.SelectedLevel = index;
                        SceneManager.LoadScene(Constants.GameSceneIndex);
                    }).AddTo(_menuSceneUiView);
            }

            if (audioController.IsPlayingMenuAudio() == false)
            {
                audioController.PlayMenuBg();
            }
        }
    }
}