using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MenuScene
{
    public class MenuSceneUiView : MonoBehaviour
    {
        [SerializeField] private Button[] levelButtons;

        public Button[] LevelButtons => levelButtons;
    }
}