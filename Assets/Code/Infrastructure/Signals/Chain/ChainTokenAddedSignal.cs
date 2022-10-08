// ReSharper disable ClassNeverInstantiated.Global класс создаются Zenject-ом
using Code.Gameplay.Tokens;
using Code.Infrastructure.BaseSignals;

namespace Code.Infrastructure.Signals.Chain
{
	public class ChainTokenAddedSignal : ImmutableSignal<Token>
	{
		public ChainTokenAddedSignal(Token value)
			: base(value) { }
	}
}