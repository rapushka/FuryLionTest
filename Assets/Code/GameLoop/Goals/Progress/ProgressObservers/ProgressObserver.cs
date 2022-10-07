using System;

namespace Code.GameLoop.Goals.Progress.ProgressObservers
{
	public abstract class ProgressObserver
	{
		public event Action<ProgressObserver> GoalReached;

		public event Action<ProgressObserver, int> GoalProgress;

		protected void GoalReachedInvoke() => GoalReached?.Invoke(this);

		protected void GoalProgressInvoke(int value) => GoalProgress?.Invoke(this, value);
	}
}