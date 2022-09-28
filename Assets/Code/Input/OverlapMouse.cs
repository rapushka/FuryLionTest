using Code.Extensions;
using Code.Infrastructure;
using UnityEngine;
using Zenject;

namespace Code.Input
{
	public class OverlapMouse : MonoBehaviour
	{
		[SerializeField] private float _overlapRadius = 0.01f;

		private SignalBus _signalBus;
		private Camera _camera;
		private bool _isPressed;
		private Collider2D[] _overlapResults;

		[Inject] public void Construct(SignalBus signalBus) => _signalBus = signalBus;

		public void EnableOverlapping()
		{
			_isPressed = true;
			_signalBus.Do(FireClickSignal, @if: AnyColliderHit());
		}

		public void DisableOverlapping() => _isPressed = false;

		private void Start()
		{
			_overlapResults = new Collider2D[1];
			_camera = Camera.main;
		}

		private void FixedUpdate() => _signalBus.Do(FireHitSignal, @if: _isPressed && AnyColliderHit());

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