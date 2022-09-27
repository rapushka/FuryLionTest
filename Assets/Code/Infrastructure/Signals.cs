// ReSharper disable ClassNeverInstantiated.Global классы создаются Zenject-ом
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
}