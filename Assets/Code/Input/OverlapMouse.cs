using System;
using Code.Workflow.Extensions;
using UnityEngine;

namespace Code.Input
{
	public class OverlapMouse : MonoBehaviour
	{
		[SerializeField] private float _overlapRadius = 0.01f;

		private Camera _camera;
		private bool _isPressed;
		private Collider2D[] _overlapResults;

		public event Action<Vector2> ClickOnToken;
		public event Action<Vector2> TokenHit;

		public void OnInputServiceOnMouseDown()
		{
			_isPressed = true;
			ClickOnToken.Do(InvokeHitEvent, @if: AnyColliderHit());
		}

		public void OnInputServiceOnMouseUp() => _isPressed = false;

		private void Start()
		{
			_overlapResults = new Collider2D[1];
			_camera = Camera.main;
		}

		private void Update() => TokenHit.Do(InvokeHitEvent, @if: _isPressed && AnyColliderHit());

		private void InvokeHitEvent(Action<Vector2> @event)
			=> _overlapResults.ForEach((r) => @event?.Invoke(r.transform.position));

		private bool AnyColliderHit() => Overlap() != 0;

		private int Overlap()
			=> Physics2D.OverlapCircleNonAlloc(MouseWorldPosition(), _overlapRadius, _overlapResults);

		private Vector2 MouseWorldPosition() => _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
	}
}