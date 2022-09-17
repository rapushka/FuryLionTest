using UnityEngine;

namespace Code
{
	public class OverlapMouse : MonoBehaviour
	{
		[SerializeField] private float _overlapRadius = 0.01f;

		private Camera _camera;
		private bool _isPressed;
		private Collider2D[] _results;

		private void Start()
		{
			_results = new Collider2D[1];
			_camera = Camera.main;
		}

		private void Update()
		{
			UpdateMouseState();
			OverlapMousePosition();
		}

		private void UpdateMouseState()
		{
			if (Input.GetMouseButtonDown(0))
			{
				_isPressed = true;
			}

			if (Input.GetMouseButtonUp(0))
			{
				_isPressed = false;
			}
		}

		private void OverlapMousePosition()
		{
			if (_isPressed == false)
			{
				return;
			}

			int hitsCount = Physics2D.OverlapCircleNonAlloc(MouseWorldPosition(), _overlapRadius, _results);
			if (hitsCount == 0)
			{
				return;
			}

			foreach (Collider2D result in _results)
			{
				Debug.Log(result.transform.position);
			}
		}

		private Vector2 MouseWorldPosition() => _camera.ScreenToWorldPoint(Input.mousePosition);
	}
}