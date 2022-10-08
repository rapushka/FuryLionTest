// ReSharper disable ClassNeverInstantiated.Global класс создаются Zenject-ом
using Code.Gameplay.Tokens;
using Code.Infrastructure.BaseSignals;

namespace Code.Infrastructure.Signals.Tokens
{
	public class TokenHitSignal : ImmutableSignal<Token>
	{
		public TokenHitSignal(Token value)
			: base(value) { }
	}
}