using System;
using Code.Services;
using Code.Workflow.Extensions;
using Unity.VisualScripting;
using UnityEngine;

namespace Code
{
	public class OverlapMouse : MonoBehaviour
	{
		[SerializeField] private float _overlapRadius = 0.01f;
		[SerializeField] private InputService _inputService;

		private Camera _camera;
		private bool _isPressed;
		private Collider2D[] _results;

		public event Action<Vector2> TokenTouched;

		private void OnEnable()
		{
			_inputService.MouseDown += OnInputServiceOnMouseDown;
			_inputService.MouseUp += OnInputServiceOnMouseUp;
		}

		private void OnInputServiceOnMouseDown() => _isPressed = true;

		private void OnInputServiceOnMouseUp() => _isPressed = false;

		private void Start()
		{
			_results = new Collider2D[1];
			_camera = Camera.main;
		}

		private void Update() => OverlapMousePosition();

		private void OverlapMousePosition()
		{
			if (_isPressed
			    && AnyHit())
			{
				_results.ForEach((r) => TokenTouched?.Invoke(r.transform.position));
			}
		}

		private bool AnyHit() => Overlap() != 0;

		private int Overlap() => Physics2D.OverlapCircleNonAlloc(MouseWorldPosition(), _overlapRadius, _results);

		private Vector2 MouseWorldPosition() => _camera.ScreenToWorldPoint(Input.mousePosition);

		private void OnDisable()
		{
			_inputService.MouseDown -= OnInputServiceOnMouseDown;
			_inputService.MouseUp -= OnInputServiceOnMouseUp;
		}
	}
}