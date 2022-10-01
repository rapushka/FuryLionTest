using Code.Extensions;
using Code.Infrastructure;
using Code.Infrastructure.Configurations;
using Code.Infrastructure.Configurations.Interfaces;
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
			_signalBus.Do(FireClickSignal, @if: AnyColliderHit());
		}

		public void DisableOverlapping() => _isPressed = false;

		public void Initialize()
		{
			_overlapResults = new Collider2D[1];
			_camera = Camera.main;
		}

		public void FixedTick() => _signalBus.Do(FireHitSignal, @if: _isPressed && AnyColliderHit());

		private void FireHitSignal(SignalBus signalBus)
			=> _overlapResults.ForEach((r) => signalBus.Fire(new TokenHitSignal(r.transform.position)));

		private void FireClickSignal(SignalBus signalBus)
			=> _overlapResults.ForEach((r) => signalBus.Fire(new TokenClickSignal(r.transform.position)));

		private bool AnyColliderHit() => Overlap() != 0;

		private int Overlap()
			=> Physics2D.OverlapCircleNonAlloc(MouseWorldPosition(), _overlapRadius, _overlapResults);

		private Vector2 MouseWorldPosition() => _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
	}
}