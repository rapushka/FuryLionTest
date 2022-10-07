using System.Collections.Generic;
using System.Linq;
using Code.GameLoop.Goals.Progress;
using Code.GameLoop.Goals.Progress.ProgressObservers;
using UnityEngine;
using Zenject;

namespace Code.UI.GoalViews
{
	public class GoalViewsContainer : IInitializable
	{
		private readonly GoalsProgress _observers;
		private readonly Transform _goalsRoot;
		private readonly ReachScoreGoalView _reachScoreViewPrefab;

		private Dictionary<ProgressObserver, GoalView> _viewsForObservers;

		[Inject]
		public GoalViewsContainer
		(
			GoalsProgress goalsProgress,
			GoalsRoot goalsRoot,
			ReachScoreGoalView reachScoreViewPrefab
		)
		{
			_reachScoreViewPrefab = reachScoreViewPrefab;
			_observers = goalsProgress;
			_goalsRoot = goalsRoot.Transform;
		}

		public void Initialize()
		{
			foreach (var observer in _observers.ProgressObservers)
			{
				if (observer is ScoreValueReachedObserver reachScoreValue)
				{
					Object.Instantiate(_reachScoreViewPrefab, _goalsRoot)
					      .Construct(reachScoreValue, reachScoreValue.TargetScoreValue);
				}
			}
		}
	}
}