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

		public event Action<Vector2> TokenTouched;
		
		public void OnInputServiceOnMouseDown() => _isPressed = true;

		public void OnInputServiceOnMouseUp() => _isPressed = false;

		private void Start()
		{
			_overlapResults = new Collider2D[1];
			_camera = Camera.main;
		}

		private void Update() => OverlapMousePosition();

		private void OverlapMousePosition()
		{
			if (_isPressed
			    && AnyColliderHit())
			{
				_overlapResults.ForEach((r) => TokenTouched?.Invoke(r.transform.position));
			}
		}

		private bool AnyColliderHit() => Overlap() != 0;

		private int Overlap() => Physics2D.OverlapCircleNonAlloc(MouseWorldPosition(), _overlapRadius, _overlapResults);

		private Vector2 MouseWorldPosition() => _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
	}
}