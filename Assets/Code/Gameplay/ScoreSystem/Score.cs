using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Tokens;
using Code.Infrastructure.Configurations.Interfaces;
using Code.Infrastructure.Signals.Goals;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.ScoreSystem
{
	public class Score : IInitializable
	{
		private readonly int _scoreMultiplier;
		private readonly float _comboMultiplier;
		private readonly SignalBus _signalBus;

		private int _currentScore;

		[Inject]
		public Score(IScoreConfig scoreSettings, SignalBus signalBus)
		{
			_scoreMultiplier = scoreSettings.ScoreMultiplier;
			_comboMultiplier = scoreSettings.MultiplierPerTokenInChain;
			_signalBus = signalBus;
		}

		public void Initialize() => InvokeValueUpdate();

		public void OnChainComposed(IEnumerable<Token> chain)
		{
			_currentScore += ScaleScore(chain.Count());
			InvokeValueUpdate();
		}

		public void OnTokensDestroyed(int count)
		{
			_currentScore += count * _scoreMultiplier / 2;
			InvokeValueUpdate();
		}

		private int ScaleScore(int chainLenght) => IntPow(chainLenght, _comboMultiplier) * _scoreMultiplier;

		private static int IntPow(int number, float power) => Mathf.CeilToInt(Mathf.Pow(number, power));

		private void InvokeValueUpdate() => _signalBus.Fire(new ScoreUpdateSignal(_currentScore));
	}
}