using Code.Workflow.Extensions;
using UnityEngine;

namespace Code.Environment
{
	public class ChainRenderer
	{
		private readonly LineRenderer _lineRenderer;

		public ChainRenderer(LineRenderer lineRenderer) => _lineRenderer = lineRenderer;

		public void OnTokenAdded(Vector2 newPosition) => _lineRenderer.AddPosition(newPosition);

		public void OnLastTokenRemoved() => _lineRenderer.RemoveLastPosition();

		public void OnChainEnded() => _lineRenderer.ClearPositions();
	}
}