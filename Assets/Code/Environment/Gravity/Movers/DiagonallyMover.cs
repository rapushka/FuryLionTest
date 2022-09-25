using System.Collections.Generic;
using UnityEngine;

namespace Code.Environment.Gravity.Movers
{
	public class DiagonallyMover : BaseDirectionMover
	{
		protected override void FallTokenAtDirection(KeyValuePair<Vector2Int, Vector3> pair)
		{
			var position = pair.Key;
			var direction = pair.Value;

			Tokens[position.x, position.y].transform.Translate(Vector3.down + direction);
			Swap(ref Tokens[position.x, position.y], ref Tokens[position.x + (int)direction.x, position.y - 1]);
		}
	}
}