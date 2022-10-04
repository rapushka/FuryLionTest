using System;
using Code.GameCycle.Goals.Conditions;

namespace Code.GameCycle.Goals.Progress.ProgressObservers
{
	public class ScoreValueReachedObserver : ProgressObserver
	{
		private int _scoreValueRemain;

		public ScoreValueReachedObserver(ReachScoreValue goal)
		{
			_scoreValueRemain = goal.TargetScoreValue;
		}

		public void OnScoreAdded(int value)
		{
			if (value <= 0)
			{
				return;
			}

			_scoreValueRemain -= value;
			
			if (_scoreValueRemain <= 0)
			{
				GoalReachedInvoke();
			}
		}
	}
}