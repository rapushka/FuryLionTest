using Code.Gameplay;
using Code.Workflow.Extensions;
using UnityEngine;

namespace Code.Environment
{
	public class ChainRenderer
	{
		private readonly LineRenderer _lineRenderer;
		private readonly Chain _chain;

		public ChainRenderer(Chain chain, LineRenderer lineRenderer)
		{
			_lineRenderer = lineRenderer;
			_chain = chain;
		}

		public void StartChain(Vector2 position)
		{
			_chain.StartComposing(position);
			_lineRenderer.AddPosition(position);
		}

		public void AddTokenToChain(Vector2 position) 
			=> position.Do(_lineRenderer.AddPosition, @if: _chain.TryAddToken);

		public void EndChain()
		{
			_lineRenderer.ClearPositions();
			_chain.EndComposing();
		}
	}
}