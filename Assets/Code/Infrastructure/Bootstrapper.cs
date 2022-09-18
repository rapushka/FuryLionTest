using Code.Environment;
using Code.Gameplay;
using Code.Input;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Infrastructure
{
	public class Bootstrapper : MonoBehaviour
	{
		[SerializeField] private LineRenderer _lineRenderer;
		[SerializeField] private OverlapMouse _overlapMouse;
		[SerializeField] private InputService _inputService;
		[FormerlySerializedAs("_field")] [SerializeField] private ChainView _chainView;
		[FormerlySerializedAs("_tokensDistance")] [SerializeField] private Field _field;

		private void Awake()
		{
			var chain = new Chain(_field);
			_chainView.Construct(chain, _lineRenderer);
		}

		private void OnEnable()
		{
			_inputService.MouseDown += _overlapMouse.EnableOverlapping;
			_inputService.MouseUp += _overlapMouse.DisableOverlapping;
			_inputService.MouseUp += _chainView.EndChain;
			
			_overlapMouse.TokenHit += _chainView.AddTokenToChain;
			_overlapMouse.ClickOnToken += _chainView.StartChain;
		}

		private void OnDisable()
		{
			_inputService.MouseDown -= _overlapMouse.EnableOverlapping;
			_inputService.MouseUp -= _overlapMouse.DisableOverlapping;
			_inputService.MouseUp -= _chainView.EndChain;
			
			_overlapMouse.TokenHit -= _chainView.AddTokenToChain;
			_overlapMouse.ClickOnToken -= _chainView.StartChain;
		}
	}
}