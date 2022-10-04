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
		}

		public void OnTokenDestroyed(TokenUnit unit)
		{
			foreach (var observer in _progressObservers)
			{
				if (observer is DestroyTokensOfTypeObserver<TokenUnit> destroyTokens)
				{
					destroyTokens.OnTokenDestroyed(unit);
				}
				else if (observer is DestroyNTokensOfColorObserver destroyTokensOfColor)
				{
					Debug.Log("он не считает обобщённый:(");
					destroyTokensOfColor.OnTokenDestroyed(unit);
				}
				else if (observer is DestroyAllObstaclesOfTypeObserver destroyObstacles)
				{
					destroyObstacles.OnTokenDestroyed(unit);
				}
			}
		}

		public void OnScoreUpdated(int value)
		{
			foreach (var observer in _progressObservers)
			{
				if (observer is ScoreValueReachedObserver destroyTokens)
				{
					destroyTokens.OnScoreAdded(value);
				}
			}
		}
	}
}