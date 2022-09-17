using Code.Environment;
using Code.Input;
using UnityEngine;

namespace Code.Infrastructure
{
	public class Bootstrapper : MonoBehaviour
	{
		[SerializeField] private LineRenderer _lineRenderer;
		[SerializeField] private OverlapMouse _overlapMouse;
		[SerializeField] private InputService _inputService;
		
		private LineDrawer _lineDrawer;

		private void Awake() => _lineDrawer = new LineDrawer(_lineRenderer);

		private void OnEnable()
		{
			_inputService.MouseDown += _overlapMouse.OnInputServiceOnMouseDown;
			_inputService.MouseUp += _overlapMouse.OnInputServiceOnMouseUp;
			
			_overlapMouse.TokenTouched += _lineDrawer.AddTokenPosition;
			_inputService.MouseUp += _lineDrawer.ClearTokens;
		}

		private void OnDisable()
		{
			_inputService.MouseDown -= _overlapMouse.OnInputServiceOnMouseDown;
			_inputService.MouseUp -= _overlapMouse.OnInputServiceOnMouseUp;
			
			_overlapMouse.TokenTouched -= _lineDrawer.AddTokenPosition;
			_inputService.MouseUp -= _lineDrawer.ClearTokens;
		}
	}
}