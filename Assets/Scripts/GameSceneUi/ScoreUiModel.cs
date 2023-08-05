using UniRx;

namespace GameSceneUi
{
    public class ScoreUiModel
    {
        public ReactiveProperty<int> Score { get; set; } = new ReactiveProperty<int>();
    }
}