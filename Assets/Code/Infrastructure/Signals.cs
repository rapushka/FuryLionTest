// ReSharper disable ClassNeverInstantiated.Global классы создаются Zenject-ом
using System.Collections.Generic;
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

	public class ChainEndedSignal : ImmutableSignal<LinkedList<Vector2>>
	{
		public ChainEndedSignal(LinkedList<Vector2> value)
			: base(value) { }
	}
}