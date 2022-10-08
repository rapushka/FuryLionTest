// ReSharper disable ClassNeverInstantiated.Global класс создаются Zenject-ом
using System.Collections.Generic;
using Code.Gameplay.Tokens;
using Code.Infrastructure.BaseSignals;

namespace Code.Infrastructure.Signals.Chain
{
	public class ChainComposedSignal : ImmutableSignal<IEnumerable<Token>>
	{
		public ChainComposedSignal(IEnumerable<Token> value)
			: base(value) { }
	}
}