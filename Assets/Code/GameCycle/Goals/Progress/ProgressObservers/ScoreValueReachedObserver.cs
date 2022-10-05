using Code.GameCycle.Goals.Conditions;

namespace Code.GameCycle.Goals.Progress.ProgressObservers
{
	public class ScoreValueReachedObserver : ProgressObserver
	{
		private readonly int _targetScoreValue;

		public ScoreValueReachedObserver(ReachScoreValue goal)
		{
			_targetScoreValue = goal.TargetScoreValue;
		}

		public void OnScoreUpdated(int value)
		{
			if (value >= _targetScoreValue)
			{
				GoalReachedInvoke();
			}
		}
	}
}