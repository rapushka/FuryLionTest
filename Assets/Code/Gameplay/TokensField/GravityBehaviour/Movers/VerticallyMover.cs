using System.Collections.Generic;
using Code.Gameplay.Tokens;
using UnityEngine;

namespace Code.Gameplay.TokensField.GravityBehaviour.Movers
{
	public class VerticallyMover : BaseDirectionMover
	{
		protected override void FallTokenAtDirection(KeyValuePair<Vector2Int, Vector3> pair)
		{
			var indexes = pair.Key;
			var direction = pair.Value;
			
			for (var y = indexes.y; y > 0 && BellowIsEmpty(Tokens, indexes.x, y); y--)
			{
				MoveToken(indexes.x, y, direction);
			}
		}

		private static bool BellowIsEmpty(Token[,] tokens, int x, int y) => tokens[x, y - 1] == false;
	}
}