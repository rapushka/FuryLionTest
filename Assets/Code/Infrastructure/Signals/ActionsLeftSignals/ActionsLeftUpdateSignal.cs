using Code.Infrastructure.BaseSignals;

namespace Code.Infrastructure.Signals.ActionsLeftSignals
{
	public class ActionsLeftUpdateSignal : ImmutableSignal<int>
	{
		public ActionsLeftUpdateSignal(int value)
			: base(value) { }
	}
}