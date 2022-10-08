// ReSharper disable ClassNeverInstantiated.Global класс создаются Zenject-ом
using Code.Infrastructure.BaseSignals;

namespace Code.Infrastructure.Signals.Goals
{
	public class ScoreUpdateSignal : ImmutableSignal<int>
	{
		public ScoreUpdateSignal(int value)
			: base(value) { }
	}
}