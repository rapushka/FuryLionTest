using Code.Infrastructure.BaseSignals;

namespace Code.Infrastructure.Signals.Coins
{
	public class CoinsCountUpdateSignal : ImmutableSignal<int>
	{
		public CoinsCountUpdateSignal(int value)
			: base(value) { }
	}
}