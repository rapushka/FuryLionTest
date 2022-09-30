using System.Collections.Generic;
using Code.Infrastructure;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.ScoreSystem
{
	public class Score
	{
		private readonly int _scoreMultiplier;
		private readonly float _multiplierPerTokenInChain;
		private readonly SignalBus _signalBus;

		private int _currentScore;

		[Inject]
		public Score(Configuration.ScoreSettings scoreSettings, SignalBus signalBus)
		{
			_scoreMultiplier = scoreSettings.ScoreMultiplier;
			_multiplierPerTokenInChain = scoreSettings.MultiplierPerTokenInChain;
			_signalBus = signalBus;
		}

		public void OnChainCompleted(LinkedList<Vector2> chain)
		{
			_currentScore += ScaleScore(chain.Count);
			_signalBus.Fire(new ScoreUpdateSignal(_currentScore));
		}

		private int ScaleScore(int chainLenght) => IntPow(chainLenght, _multiplierPerTokenInChain) * _scoreMultiplier;

		private static int IntPow(int number, float power) => Mathf.CeilToInt(Mathf.Pow(number, power));
	}
}