using System;
using Code.Extensions;
using Code.Gameplay.Tokens;
using Code.Infrastructure.BaseSignals;
using Code.Infrastructure.Configurations.Interfaces;
using Code.Infrastructure.Signals.Tokens;
using UnityEngine;
using Zenject;

namespace Code.Input
{
	public class OverlapMouse : IInitializable, IFixedTickable
	{
		private readonly SignalBus _signalBus;
		private readonly float _overlapRadius;

		private Camera _camera;
		private bool _isPressed;
		private Collider2D[] _overlapResults;

		[Inject]
		public OverlapMouse(SignalBus signalBus, IInputConfig inputSettings)
		{
			_signalBus = signalBus;
			_overlapRadius = inputSettings.CursorOverlapRadius;
		}

		public void EnableOverlapping()
		{
			_isPressed = true;

			if (AnyColliderHit())
			{
				FirePressSignal();
			}
		}

		public void DisableOverlapping()
		{
			_isPressed = false;

			if (AnyColliderHit())
			{
				FireClickSignal();
			}
		}

		public void Initialize()
		{
			_overlapResults = new Collider2D[1];
			_camera = Camera.main;
		}

		public void FixedTick() => _signalBus.Do(FireHitSignal, @if: _isPressed && AnyColliderHit());

		private void FireHitSignal()
			=> _overlapResults.ForEach((r) => _signalBus.Fire(new TokenHitSignal(r.GetComponent<Token>())));

		private void FirePressSignal() => FireSignalForToken((t) => new TokenPressSignal(t));

		private void FireClickSignal() => FireSignalForToken((t) => new TokenClickSignal(t));

		private void FireSignalForToken<TSignal>(Func<Token, TSignal> action)
			where TSignal : ImmutableSignal<Token>
			=> _overlapResults.ForEach((r) => _signalBus.Fire(action(r.GetComponent<Token>())));

		private bool AnyColliderHit() => Overlap() != 0;

		private int Overlap() => Physics2D.OverlapCircleNonAlloc(MouseWorldPosition(), _overlapRadius, _overlapResults);

		private Vector2 MouseWorldPosition() => _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
	}
}