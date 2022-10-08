using Code.Infrastructure.BaseSignals;

namespace Code.Infrastructure.Signals.Tokens
{
	public class TokensDestroyedByBonusSignal : ImmutableSignal<int>
	{
		public TokensDestroyedByBonusSignal(int value)
			: base(value) { }
	}
}