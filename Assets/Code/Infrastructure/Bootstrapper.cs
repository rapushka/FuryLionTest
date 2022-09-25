using Code.Environment;
using Code.Environment.GravityBehaviour;
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
		[SerializeField] private TokensCollection _tokens;
		[SerializeField] private TokensSpawner _spawner;

		private ChainRenderer _chainRenderer;
		private Chain _chain;

		private void Awake()
		{
			var gravity = new Gravity();
			var tokensToTypes = _tokens.InitializedDictionary();
			
			_chain = new Chain(_field);
			_chainRenderer = new ChainRenderer(_lineRenderer);

			_spawner.Construct(tokensToTypes);
			_levelGenerator.Construct(tokensToTypes);
			_field.Construct(_levelGenerator, gravity, _spawner);
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