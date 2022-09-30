using TMPro;
using Zenject;

namespace Code.View
{
	public class ScoreView
	{
		private readonly TextMeshProUGUI _scoreText;

		[Inject]
		public ScoreView(TextMeshProUGUI scoreText) => _scoreText = scoreText;

		public void OnScoreUpdate(int newScoreValue) => _scoreText.text = newScoreValue.ToString();
	}
}