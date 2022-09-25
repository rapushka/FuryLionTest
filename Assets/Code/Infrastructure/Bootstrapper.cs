using Code.Environment;
using Code.Environment.Gravity;
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
		[SerializeField] private LevelGenerator _levelGenerator;

		private ChainRenderer _chainRenderer;
		private Chain _chain;

		private void Awake()
		{
			var gravity = new Gravity();
			
			_chain = new Chain(_field);
			_chainRenderer = new ChainRenderer(_lineRenderer);
			_field.Construct(_levelGenerator, gravity);
		}

		private void OnEnable()
		{
			_inputService.MouseDown += _overlapMouse.EnableOverlapping;
			_inputService.MouseUp += _overlapMouse.DisableOverlapping;

			_overlapMouse.ClickOnToken += _chain.StartComposing;
			_overlapMouse.TokenHit += _chain.NextToken;
			_inputService.MouseUp += _chain.EndComposing;

			_chain.TokenAdded += _chainRenderer.OnTokenAdded;
			_chain.LastTokenRemoved += _chainRenderer.OnLastTokenRemoved;
			_chain.ChainEnded += _chainRenderer.OnChainEnded;
			
			_chain.ChainEnded += _field.OnChainEnded;
		}

		private void OnDisable()
		{
			_inputService.MouseDown -= _overlapMouse.EnableOverlapping;
			_inputService.MouseUp -= _overlapMouse.DisableOverlapping;
			
			_overlapMouse.ClickOnToken -= _chain.StartComposing;
			_overlapMouse.TokenHit -= _chain.NextToken;
			_inputService.MouseUp -= _chain.EndComposing;
			
			_chain.TokenAdded -= _chainRenderer.OnTokenAdded;
			_chain.LastTokenRemoved -= _chainRenderer.OnLastTokenRemoved;
			_chain.ChainEnded -= _chainRenderer.OnChainEnded;
			
			_chain.ChainEnded -= _field.OnChainEnded;
		}
	}
}