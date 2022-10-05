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
		
		private readonly List<ProgressObserver> _markForDeleting;

		[Inject]
		public GoalsProgress(Level currentLevel, ObserversFactory observersFactory)
		{
			_progressObservers = observersFactory.GenerateObserversListFor(currentLevel.Goals);
			
			_markForDeleting = new List<ProgressObserver>();
			Subscribe();
		}

		private void Subscribe()
		{
			foreach (var observer in _progressObservers)
			{
				observer.GoalReached += OnGoalReached;
			}
		}

		private void OnGoalReached(ProgressObserver sender)
		{
			Debug.Log("Цель достигнута!");
			_markForDeleting.Add(sender);
		}

		public void OnTokenDestroyed(Token token)
		{
			foreach (var observer in _progressObservers)
			{
				if (observer is DestroyTokensOfTypeObserver destroyTokens)
				{
					destroyTokens.OnTokenDestroyed(token.TokenUnit);
				}
			}
			
			RemoveReachedGoals();
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

			RemoveReachedGoals();
		}

		private void RemoveReachedGoals()
		{
			foreach (var observer in _markForDeleting)
			{
				_progressObservers.Remove(observer);
			}
			_markForDeleting.Clear();
		}
	}
}