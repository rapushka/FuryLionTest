using Code.Gameplay.Tokens;
using Code.Infrastructure.BaseSignals;

namespace Code.Infrastructure.Signals.Bonuses
{
	public class BonusSpawnedSignal : ImmutableSignal<Token>
	{
		public BonusSpawnedSignal(Token value)
			: base(value) { }
	}
}