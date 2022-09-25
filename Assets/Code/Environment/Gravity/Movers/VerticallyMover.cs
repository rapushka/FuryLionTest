using System.Collections.Generic;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment.Gravity.Movers
{
	public class VerticallyMover : BaseDirectionMover
	{
		protected override void FallTokenAtDirection(KeyValuePair<Vector2Int, Vector3> pair)
		{
			var position = pair.Key;
			var direction = pair.Value;
			for (var y = position.y; y > 0 && BellowIsEmpty(Tokens, position.x, y); y--)
			{
				Tokens[position.x, y].transform.Translate(direction);
				Swap(ref Tokens[position.x, y], ref Tokens[position.x, y - 1]);
			}
		}

		private static bool BellowIsEmpty(Token[,] tokens, int x, int y) => tokens[x, y - 1] == false;
	}
}