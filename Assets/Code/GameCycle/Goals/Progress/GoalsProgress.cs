using System.Collections.Generic;
using Code.GameCycle.Goals.Progress.ProgressObservers;
using Code.Gameplay.Tokens;
using Code.Levels;
using UnityEngine;
using Zenject;

namespace Code.GameCycle.Goals.Progress
{
	public class GoalsProgress : IInitializable
	{
		private readonly Level _currentLevel;
		private readonly ObserversFactory _observersFactory;
		
		private List<ProgressObserver> _progressObservers;
		private List<ProgressObserver> _markForDeleting;

		[Inject]
		public GoalsProgress(Level currentLevel, ObserversFactory observersFactory)
		{
			_currentLevel = currentLevel;
			_observersFactory = observersFactory;

		}

		public void Initialize()
		{
			// _progressObservers = new List<ProgressObserver>();
			_markForDeleting = new List<ProgressObserver>();
			_progressObservers = _observersFactory.GenerateObserversListFor(_currentLevel.Goals);
			Subscribe();
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