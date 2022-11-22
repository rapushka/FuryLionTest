using Code.GameLoop.Goals.Progress.ProgressObservers;
using Code.Infrastructure.BaseSignals;

namespace Code.Infrastructure.Signals.Goals
{
	public class GoalReachedSignal : ImmutableSignal<ProgressObserver>
	{
		public GoalReachedSignal(ProgressObserver value) 
			: base(value) { }
	}
}