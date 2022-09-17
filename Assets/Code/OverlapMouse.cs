using System;
using Code.Services;
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

		private void Update() => Overlap();

		private void Overlap()
		{
			if (_isPressed == false)
			{
				return;
			}

			var hitsCount = Physics2D.OverlapCircleNonAlloc(MouseWorldPosition(), _overlapRadius, _results);
			if (hitsCount == 0)
			{
				return;
			}

			foreach (Collider2D result in _results)
			{
				TokenTouched?.Invoke(result.transform.position);
			}
		}

		private Vector2 MouseWorldPosition() => _camera.ScreenToWorldPoint(Input.mousePosition);

		private void OnDisable()
		{
			_inputService.MouseDown -= OnInputServiceOnMouseDown;
			_inputService.MouseUp -= OnInputServiceOnMouseUp;
		}
	}
}