// ReSharper disable ClassNeverInstantiated.Global класс создаются Zenject-ом
using Code.Gameplay.Tokens;
using Code.Infrastructure.BaseSignals;

namespace Code.Infrastructure.Signals.Tokens
{
	public class TokenClickSignal : ImmutableSignal<Token>
	{
		public TokenClickSignal(Token value)
			: base(value) { }
	}
}