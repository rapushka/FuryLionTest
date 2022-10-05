// ReSharper disable ClassNeverInstantiated.Global классы создаются Zenject-ом
using System.Collections.Generic;
using Code.Gameplay.Tokens;
using Code.Infrastructure.BaseSignals;
using UnityEngine;

namespace Code.Infrastructure
{
	public class MouseDownSignal { }

	public class MouseUpSignal { }

	public class TokenHitSignal : ImmutableSignal<Vector2>
	{
		public TokenHitSignal(Vector2 value)
			: base(value) { }
	}

	public class TokenClickSignal : ImmutableSignal<Vector2>
	{
		public TokenClickSignal(Vector2 value)
			: base(value) { }
	}

	public class ChainTokenAddedSignal : ImmutableSignal<Vector2>
	{
		public ChainTokenAddedSignal(Vector2 value)
			: base(value) { }
	}

	public class ChainLastTokenRemovedSignal { }

	public class ChainEndedSignal : ImmutableSignal<IEnumerable<Vector2>>
	{
		public ChainEndedSignal(IEnumerable<Vector2> value)
			: base(value) { }
	}

	public class ScoreUpdateSignal : ImmutableSignal<int>
	{
		public ScoreUpdateSignal(int value)
			: base(value) { }
	}

	public class ChainComposedSignal : ImmutableSignal<IEnumerable<Vector2>>
	{
		public ChainComposedSignal(IEnumerable<Vector2> value)
			: base(value) { }
	}

	public class LevelLostSignal { }

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
}