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
		[SerializeField] private Field _field;

		private void Awake()
		{
			_field.Construct(_lineRenderer);
		}

		private void OnEnable()
		{
			_inputService.MouseDown += _overlapMouse.EnableOverlapping;
			_inputService.MouseUp += _overlapMouse.DisableOverlapping;
			_inputService.MouseUp += _field.EndChain;
			
			_overlapMouse.TokenHit += _field.AddTokenToChain;
			_overlapMouse.ClickOnToken += _field.StartChain;
		}

		private void OnDisable()
		{
			_inputService.MouseDown -= _overlapMouse.EnableOverlapping;
			_inputService.MouseUp -= _overlapMouse.DisableOverlapping;
			_inputService.MouseUp -= _field.EndChain;
			
			_overlapMouse.TokenHit -= _field.AddTokenToChain;
			_overlapMouse.ClickOnToken -= _field.StartChain;
		}
	}
}