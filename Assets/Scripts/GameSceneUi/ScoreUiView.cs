using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameSceneUi
{
    public class ScoreUiView : MonoBehaviour
    {
        [SerializeField] private Transform imageIcon;
        [SerializeField] private TextMeshProUGUI scoreText;

        public void SetScore(int score)
        {
            imageIcon.DORotate(Vector3.up * 360, 1);
            scoreText.text = score.ToString();
        }

        public void Hide()
        {
            imageIcon.DOScale(0, 0.25f).SetEase(Ease.InOutBounce);
            scoreText.DOFade(0, 0.25f).SetEase(Ease.Linear);
        }
    }
}