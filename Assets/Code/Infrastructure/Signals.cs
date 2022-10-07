// ReSharper disable ClassNeverInstantiated.Global классы создаются Zenject-ом
using System.Collections.Generic;
using Code.Gameplay.Tokens;
using Code.Infrastructure.BaseSignals;

namespace Code.Infrastructure
{
	public class MouseDownSignal { }

	public class MouseUpSignal { }

	public class TokenHitSignal : ImmutableSignal<Token>
	{
		public TokenHitSignal(Token value)
			: base(value) { }
	}

	public class TokenClickSignal : ImmutableSignal<Token>
	{
		public TokenClickSignal(Token value)
			: base(value) { }
	}

	public class ChainTokenAddedSignal : ImmutableSignal<Token>
	{
		public ChainTokenAddedSignal(Token value)
			: base(value) { }
	}

	public class ChainLastTokenRemovedSignal { }

	public class ChainEndedSignal : ImmutableSignal<IEnumerable<Token>>
	{
		public ChainEndedSignal(IEnumerable<Token> value)
			: base(value) { }
	}

	public class ScoreUpdateSignal : ImmutableSignal<int>
	{
		public ScoreUpdateSignal(int value)
			: base(value) { }
	}

	public class ChainComposedSignal : ImmutableSignal<IEnumerable<Token>>
	{
		public ChainComposedSignal(IEnumerable<Token> value)
			: base(value) { }
	}

	public class GameLoseSignal { }

	public class GameVictorySignal { }

	public class RemainingActionsUpdateSignal : ImmutableSignal<int>
	{
		public RemainingActionsUpdateSignal(int value)
			: base(value) { }
	}

	public class BonusSpawnedSignal : ImmutableSignal<Token>
	{
		public BonusSpawnedSignal(Token value)
			: base(value) { }
	}

	public class TokenDestroyedSignal : ImmutableSignal<Token>
	{
		public TokenDestroyedSignal(Token token)
			: base(token) { }
	}
	
	public class ResetButtonClickSignal { }
	
	public class AllGoalsReachedSignal { }

	public class ActionsOverSignal { }
}