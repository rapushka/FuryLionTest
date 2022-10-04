using System.Collections.Generic;
using Code.GameCycle.Goals.Progress.ProgressObservers;
using Code.Gameplay.Tokens;
using Code.Levels;
using UnityEngine;
using Zenject;

namespace Code.GameCycle.Goals.Progress
{
	public class GoalsProgress
	{
		private readonly List<ProgressObserver> _progressObservers;

		[Inject]
		public GoalsProgress(Level currentLevel, ObserversFactory observersFactory)
		{
			_progressObservers = observersFactory.GenerateObserversListFor(currentLevel.Goals);
			Subscribe();
		}

		private void Subscribe()
		{
			foreach (var observer in _progressObservers)
			{
				observer.GoalReached += OnGoalReached;
			}
		}

		private void OnGoalReached()
		{
			Debug.Log("Цель достигнута!");
		}

		public void OnTokenDestroyed(TokenUnit unit)
		{
			foreach (var observer in _progressObservers)
			{
				if (observer is DestroyTokensOfTypeObserver destroyTokens)
				{
					destroyTokens.OnTokenDestroyed(unit);
				}
			}
		}

		public void OnScoreUpdate(int value)
		{
			foreach (var observer in _progressObservers)
			{
				if (observer is ScoreValueReachedObserver destroyTokens)
				{
					destroyTokens.OnScoreUpdated(value);
				}
			}
		}
	}
}