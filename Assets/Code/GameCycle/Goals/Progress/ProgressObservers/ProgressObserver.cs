using System;

namespace Code.GameCycle.Goals.Progress.ProgressObservers
{
	public abstract class ProgressObserver
	{
		public event Action GoalReached;

		protected void GoalReachedInvoke() => GoalReached?.Invoke();
	}
}