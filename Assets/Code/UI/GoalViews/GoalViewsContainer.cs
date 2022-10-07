using System.Collections.Generic;
using Code.Extensions;
using Code.GameLoop.Goals.Progress;
using Code.GameLoop.Goals.Progress.ProgressObservers;
using Code.Gameplay.Tokens;
using UnityEngine;
using Zenject;

namespace Code.UI.GoalViews
{
	public class GoalViewsContainer : IInitializable
	{
		private readonly GoalsProgress _observers;
		private readonly Transform _goalsRoot;
		private readonly ReachScoreGoalView _reachScoreViewPrefab;
		private readonly DestroyTokensGoalView _destroyTokensViewPrefab;
		private readonly Dictionary<TokenUnit, Token> _tokens;

		private Dictionary<ProgressObserver, GoalView> _viewsForObservers;

		[Inject]
		public GoalViewsContainer
		(
			GoalsProgress goalsProgress,
			GoalsRoot goalsRoot,
			ReachScoreGoalView reachScoreViewPrefab,
			DestroyTokensGoalView destroyTokensViewPrefab,
			TokensCollection tokens
		)
		{
			_observers = goalsProgress;
			_goalsRoot = goalsRoot.Transform;
			_reachScoreViewPrefab = reachScoreViewPrefab;
			_destroyTokensViewPrefab = destroyTokensViewPrefab;
			_tokens = tokens.AsDictionary();
		}

		public void Initialize() => CreateViewsForGoalsObservers();

		private void CreateViewsForGoalsObservers() => _observers.ProgressObservers.ForEach(CreateViewForEntry);

		private void CreateViewForEntry(ProgressObserver observer)
		{
			if (observer is ScoreValueReachedObserver scoreValueReachedObserver)
			{
				CreateScoreReachGoal(scoreValueReachedObserver);
			}
			else if (observer is DestroyTokensOfTypeObserver destroyTokensOfTypeObserver)
			{
				CreateDestroyTokensGoal(destroyTokensOfTypeObserver);
			}
		}

		private void CreateScoreReachGoal(ScoreValueReachedObserver observer)
		{
			Object.Instantiate(_reachScoreViewPrefab, _goalsRoot)
			      .Construct(observer, observer.TargetScoreValue);
		}

		private void CreateDestroyTokensGoal(DestroyTokensOfTypeObserver observer)
		{
			var sprite = GetSpriteForToken(observer.TargetUnit);
			var targetCount = observer.TargetCount;

			Object.Instantiate(_destroyTokensViewPrefab, _goalsRoot)
			      .Construct(observer, targetCount, sprite);
		}

		private Sprite GetSpriteForToken(TokenUnit unit) => _tokens[unit].Sprite;
	}
}