using System.Collections.Generic;
using Code.Extensions;
using UnityEngine;

namespace Code.Environment
{
	public class ChainRenderer
	{
		private readonly LineRenderer _lineRenderer;

		public ChainRenderer(LineRenderer lineRenderer) => _lineRenderer = lineRenderer;

		public void OnTokenAdded(Vector2 newPosition) => _lineRenderer.AddPosition(newPosition);

		public void OnLastTokenRemoved() => _lineRenderer.RemoveLastPosition();

		public void OnChainEnded(LinkedList<Vector2> linkedList) => _lineRenderer.ClearPositions();
	}
}