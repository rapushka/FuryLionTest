using System.Collections.Generic;
using Code.Infrastructure;
using Code.Infrastructure.Configurations;
using Code.Infrastructure.Configurations.Interfaces;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.ScoreSystem
{
	public class Score : IInitializable
	{
		private readonly int _scoreMultiplier;
		private readonly float _multiplierPerTokenInChain;
		private readonly SignalBus _signalBus;

		private int _currentScore;

		[Inject]
		public Score(IScoreConfig scoreSettings, SignalBus signalBus)
		{
			_scoreMultiplier = scoreSettings.ScoreMultiplier;
			_multiplierPerTokenInChain = scoreSettings.MultiplierPerTokenInChain;
			_signalBus = signalBus;
		}

		public void Initialize() => InvokeValueUpdate();

		public void OnChainComposed(LinkedList<Vector2> chain)
		{
			_currentScore += ScaleScore(chain.Count);
			InvokeValueUpdate();
		}

		private int ScaleScore(int chainLenght) => IntPow(chainLenght, _multiplierPerTokenInChain) * _scoreMultiplier;

		private static int IntPow(int number, float power) => Mathf.CeilToInt(Mathf.Pow(number, power));
		
		private void InvokeValueUpdate() => _signalBus.Fire(new ScoreUpdateSignal(_currentScore));
	}
}