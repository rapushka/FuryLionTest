using System.Collections.Generic;
using System.Linq;
using Code.GameLoop.Goals.Progress.ProgressObservers;
using Code.Gameplay.Tokens;
using Code.Infrastructure;
using Code.Levels;
using Zenject;

namespace Code.GameLoop.Goals.Progress
{
	public class GoalsProgress : IInitializable
	{
		private readonly Level _currentLevel;
		private readonly ObserversFactory _observersFactory;
		private readonly SignalBus _signalBus;

		private List<ProgressObserver> _progressObservers;
		private List<ProgressObserver> _markForDeleting;

		[Inject]
		public GoalsProgress(Level currentLevel, ObserversFactory observersFactory, SignalBus signalBus)
		{
			_currentLevel = currentLevel;
			_observersFactory = observersFactory;
			_signalBus = signalBus;
		}

		public void Initialize()
		{
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
				if (observer is ScoreValueReachedObserver reachScore)
				{
					reachScore.OnScoreUpdated(value);
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
			_markForDeleting.Add(sender);
		}

		private void RemoveReachedGoals()
		{
			foreach (var observer in _markForDeleting)
			{
				_progressObservers.Remove(observer);
			}

			_markForDeleting.Clear();

			CheckOnAllGoalsReached();
		}

		private void CheckOnAllGoalsReached()
		{
			if (_progressObservers.Any() == false)
			{
				_signalBus.Fire<AllGoalsReachedSignal>();
			}
		}
	}
}