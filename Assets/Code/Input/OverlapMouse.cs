using System;
using Code.Extensions;
using Code.Infrastructure;
using Code.Infrastructure.BaseSignals;
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

		public event Action<Vector2> ClickOnToken;
		
		[Inject] public void Construct(SignalBus signalBus) => _signalBus = signalBus;

		public void EnableOverlapping()
		{
			_isPressed = true;
			ClickOnToken.Do(InvokeHitEvent, @if: AnyColliderHit());
		}

		public void DisableOverlapping() => _isPressed = false;

		private void Start()
		{
			_overlapResults = new Collider2D[1];
			_camera = Camera.main;
		}

		private void FixedUpdate()
		{
			if (_isPressed == false
			    || AnyColliderHit() == false)
			{
				return;
			}

			foreach (var result in _overlapResults)
			{
				_signalBus.Fire(new TokenHitSignal(result.transform.position));
			}
		}

		private void InvokeHitEvent(Action<Vector2> @event)
			=> _overlapResults.ForEach((r) => @event?.Invoke(r.transform.position));

		private bool AnyColliderHit() => Overlap() != 0;

		private int Overlap()
			=> Physics2D.OverlapCircleNonAlloc(MouseWorldPosition(), _overlapRadius, _overlapResults);

		private Vector2 MouseWorldPosition() => _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
	}
}