using System;

namespace Code.GameLoop.Goals.Progress.ProgressObservers
{
	public abstract class ProgressObserver
	{
		public event Action<ProgressObserver> GoalReached;

		protected void GoalReachedInvoke() => GoalReached?.Invoke(this);
	}
}