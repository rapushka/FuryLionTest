using Code.Infrastructure.BaseSignals;

namespace Code.Infrastructure.Signals.Coins
{
	public class ActionsBoughtSignal : ImmutableSignal<int>
	{
		public ActionsBoughtSignal(int value) : base(value) { }
	}
}