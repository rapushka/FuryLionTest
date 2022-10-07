using TMPro;
using UnityEngine;

namespace Code.View
{
	public class ScoreView : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _scoreText;

		public void OnScoreUpdate(int newScoreValue) => _scoreText.text = newScoreValue.ToString("N0");
	}
}