using Code.Gameplay.Tokens;
using Code.Infrastructure.BaseSignals;

namespace Code.Infrastructure.Signals.Tokens
{
	public class TokenDestroyedSignal : ImmutableSignal<Token>
	{
		public TokenDestroyedSignal(Token token)
			: base(token) { }
	}
}