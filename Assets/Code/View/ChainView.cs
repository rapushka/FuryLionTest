using Code.Extensions;
using Code.Gameplay.Tokens;
using UnityEngine;
using Zenject;

namespace Code.View
{
	public class ChainView
	{
		private readonly LineRenderer _lineRenderer;

		[Inject] public ChainView(LineRenderer lineRenderer) => _lineRenderer = lineRenderer;

		public void OnTokenAdded(Token newToken) => _lineRenderer.AddPosition(newToken.transform.position);

		public void OnLastTokenRemoved() => _lineRenderer.RemoveLastPosition();

		public void OnChainEnded() => _lineRenderer.ClearPositions();
	}
}