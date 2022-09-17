using Code.Workflow.Extensions;
using UnityEngine;

namespace Code.Environment
{
	public class LineDrawer
	{
		private readonly LineRenderer _lineRenderer;

		public LineDrawer(LineRenderer lineRenderer)
		{
			_lineRenderer = lineRenderer;
		}

		public void AddTokenPosition(Vector2 position) => _lineRenderer.AddPosition(position);

		public void ClearTokens() => _lineRenderer.ClearPositions();
	}
}