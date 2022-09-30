using System.Collections.Generic;
using Code.Infrastructure;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.ScoreSystem
{
	public class Score
	{
		private readonly TextMeshProUGUI _scoreText;
		private readonly int _minTokensCountForChain;
		private readonly int _scoreMultiplier;
		private readonly float _multiplierPerTokenInChain;

		private int _currentScore;

		[Inject]
		public Score
		(
			Configuration.ChainParameters chainParameters,
			Configuration.ScoreSettings scoreSettings,
			TextMeshProUGUI scoreText
		)
		{
			_scoreText = scoreText;
			_minTokensCountForChain = chainParameters.MinTokensCountForChain;
			_scoreMultiplier = scoreSettings.ScoreMultiplier;
			_multiplierPerTokenInChain = scoreSettings.MultiplierPerTokenInChain;
		}

		public void OnChainEnded(LinkedList<Vector2> chain)
		{
			var chainLenght = chain.Count;
			if (chainLenght < _minTokensCountForChain)
			{
				return;
			}

			_currentScore += IntPow(chainLenght, _multiplierPerTokenInChain) * _scoreMultiplier;
			_scoreText.text = _currentScore.ToString();
		}

		private static int IntPow(int number, float power) => Mathf.CeilToInt(Mathf.Pow(number, power));
	}
}