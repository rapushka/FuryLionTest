using System;
using System.Collections.Generic;
using System.Linq;
using Code.GameLoop.Goals.Conditions;
using Code.GameLoop.Goals.Progress.ProgressObservers;
using Code.Gameplay.Tokens;
using Code.Infrastructure;
using Code.Levels;
using Zenject;

namespace Code.GameLoop.Goals.Progress
{
	public class GoalsProgress : IInitializable, IDisposable
	{
		private readonly List<Goal> _goals;
		private readonly ObserversCreator _observersCreator;
		private readonly SignalBus _signalBus;

		private List<ProgressObserver> _progressObservers;
		private List<ProgressObserver> _markForDeleting;

		public IEnumerable<ProgressObserver> ProgressObservers => _progressObservers;

		[Inject]
		public GoalsProgress(Level currentLevel, ObserversCreator observersCreator, SignalBus signalBus)
		{
			_goals = currentLevel.Goals;
			_observersCreator = observersCreator;
			_signalBus = signalBus;
		}

		public void Initialize()
		{
			_markForDeleting = new List<ProgressObserver>();
			_progressObservers = _observersCreator.CreateObserversListFor(_goals);
			Subscribe();
		}

		public void Dispose() => Unsubscribe();

		public void OnTokenDestroyed(Token token) 
			=> CheckObserversOfType<DestroyTokensOfTypeObserver, Token>(token, InvokeOnTokenDestroyed);

		public void OnScoreUpdate(int value) 
			=> CheckObserversOfType<ScoreValueReachedObserver, int>(value, InvokeOnScoreUpdated);

		private void Subscribe() => _progressObservers.ForEach((o) => o.GoalReached += OnGoalReached);

		private void Unsubscribe() => _progressObservers.ForEach((o) => o.GoalReached -= OnGoalReached);

		private void CheckObserversOfType<TObserver, TParam>
			(TParam param, Action<TObserver, TParam> action)
		{
			foreach (var observer in _progressObservers)
			{
				if (observer is TObserver tObserver)
				{
					action.Invoke(tObserver, param);
				}
			}

			RemoveReachedGoals();
		}

		private void InvokeOnScoreUpdated(ScoreValueReachedObserver observer, int value)
			=> observer.OnScoreUpdated(value);

		private void InvokeOnTokenDestroyed(DestroyTokensOfTypeObserver observer, Token param) 
			=> observer.OnTokenDestroyed(param.TokenUnit);

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