using Code.Gameplay;
using Code.Workflow.Extensions;
using UnityEngine;

namespace Code.Environment
{
	public class ChainView : MonoBehaviour
	{
		private LineRenderer _lineRenderer;
		private Chain _chain;

		public void Construct(Chain chain, LineRenderer lineRenderer)
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