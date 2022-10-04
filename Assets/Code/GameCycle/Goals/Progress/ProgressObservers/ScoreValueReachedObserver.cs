using System;
using Code.GameCycle.Goals.Conditions;

namespace Code.GameCycle.Goals.Progress.ProgressObservers
{
	public class ScoreValueReachedObserver : ProgressObserver
	{
		private readonly int _targetScoreValue;

		private int _currentScoreValue;

		public ScoreValueReachedObserver(ReachScoreValue goal)
		{
			_targetScoreValue = goal.TargetScoreValue;
		}

		public void ScoreAdded(int value)
		{
			if (value <= 0)
			{
				throw new InvalidOperationException("Score can't be negative");
			}

			_currentScoreValue += value;
			if (_currentScoreValue >= _targetScoreValue)
			{
				GoalReachedInvoke();
			}
		}
	}
}