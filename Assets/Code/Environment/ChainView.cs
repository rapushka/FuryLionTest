using Code.Extensions;
using UnityEngine;
using Zenject;

namespace Code.Environment
{
	public class ChainView
	{
		private readonly LineRenderer _lineRenderer;

		[Inject] public ChainView(LineRenderer lineRenderer) => _lineRenderer = lineRenderer;

		public void OnTokenAdded(Vector2 newPosition) => _lineRenderer.AddPosition(newPosition);

		public void OnLastTokenRemoved() => _lineRenderer.RemoveLastPosition();

		public void OnChainEnded() => _lineRenderer.ClearPositions();
	}
}