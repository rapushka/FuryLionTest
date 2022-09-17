using System.Collections.Generic;
using Code.Workflow.Extensions;
using UnityEngine;

namespace Code
{
	public class LineDrawer
	{
		private readonly LineRenderer _lineRenderer;
		private readonly List<Vector2> _cashedPositions;

		public LineDrawer(LineRenderer lineRenderer)
		{
			_lineRenderer = lineRenderer;
			_cashedPositions = new List<Vector2>();
		}

		public void OnTokenTouched(Vector2 position)
		{
			if (_cashedPositions.Contains(position))
			{
				return;
			}
			
			_cashedPositions.Add(position);
			_lineRenderer.Add(position);
		}
	}
}