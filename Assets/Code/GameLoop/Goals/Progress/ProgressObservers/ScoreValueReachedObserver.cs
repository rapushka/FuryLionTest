using Code.GameLoop.Goals.Conditions;

namespace Code.GameLoop.Goals.Progress.ProgressObservers
{
	public class ScoreValueReachedObserver : ProgressObserver
	{
		public int TargetScoreValue { get; }

		public ScoreValueReachedObserver(ReachScoreValue goal)
			: base(goal)
		{
			TargetScoreValue = goal.TargetScoreValue;
		}

		public void OnScoreUpdated(int value)
		{
			GoalProgressInvoke(value);
			if (value >= TargetScoreValue)
			{
				GoalReachedInvoke();
			}
		}
	}
}