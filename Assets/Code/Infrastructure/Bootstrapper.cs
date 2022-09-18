using Code.Environment;
using Code.Gameplay;
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

		private ChainRenderer _chainRenderer;
		
		private void Awake()
		{
			var chain = new Chain(_field);
			_chainRenderer = new ChainRenderer(chain, _lineRenderer);
		}

		private void OnEnable()
		{
			_inputService.MouseDown += _overlapMouse.EnableOverlapping;
			_inputService.MouseUp += _overlapMouse.DisableOverlapping;
			_inputService.MouseUp += _chainRenderer.EndChain;
			
			_overlapMouse.TokenHit += _chainRenderer.AddTokenToChain;
			_overlapMouse.ClickOnToken += _chainRenderer.StartChain;
		}

		private void OnDisable()
		{
			_inputService.MouseDown -= _overlapMouse.EnableOverlapping;
			_inputService.MouseUp -= _overlapMouse.DisableOverlapping;
			_inputService.MouseUp -= _chainRenderer.EndChain;
			
			_overlapMouse.TokenHit -= _chainRenderer.AddTokenToChain;
			_overlapMouse.ClickOnToken -= _chainRenderer.StartChain;
		}
	}
}