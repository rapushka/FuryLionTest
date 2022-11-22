using System;
using Code.GameLoop.Goals.Conditions;

namespace Code.GameLoop.Goals.Progress.ProgressObservers
{
	public abstract class ProgressObserver
	{
		public Goal Goal { get; }

		public event Action<ProgressObserver> GoalReached;

		public event Action<ProgressObserver, int> GoalProgress;

		protected ProgressObserver(Goal goal) => Goal = goal;

		protected void GoalReachedInvoke() => GoalReached?.Invoke(this);

		protected void GoalProgressInvoke(int value) => GoalProgress?.Invoke(this, value);
	}
}