using Code.Environment;
using Code.Input;
using TMPro;
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
			_inputService.MouseUp += _lineDrawer.ClearTokens;
			
			_overlapMouse.TokenHit += _lineDrawer.AddTokenPosition;
			_overlapMouse.ClickOnToken += (p) => Debug.Log("Click on " + p);
		}

		private void OnDisable()
		{
			_inputService.MouseDown -= _overlapMouse.OnInputServiceOnMouseDown;
			_inputService.MouseUp -= _overlapMouse.OnInputServiceOnMouseUp;
			_inputService.MouseUp -= _lineDrawer.ClearTokens;
			
			_overlapMouse.TokenHit -= _lineDrawer.AddTokenPosition;
		}
	}
}