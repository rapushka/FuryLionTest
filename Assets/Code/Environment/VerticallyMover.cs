using System.Collections.Generic;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment
{
	public class VerticallyMover
	{
		public Token[,] Move(Token[,] tokens, IEnumerable<Vector2Int> indexes)
		{
			foreach (var entry in indexes)
			{
				FallVerticallyToken(tokens, entry);
			}
			return tokens;
		}
		
		private void FallVerticallyToken(Token[,] tokens, Vector2Int indexes)
		{
			for (var y = indexes.y; y > 0 && BellowIsEmpty(tokens, indexes.x, y); y--)
			{
				tokens[indexes.x, y].transform.Translate(Vector3.down);

				Swap(ref tokens[indexes.x, y], ref tokens[indexes.x, y - 1]);
			}
		}

		private static bool BellowIsEmpty(Token[,] tokens, int x, int y) => tokens[x, y - 1] == false;

		private void Swap(ref Token left, ref Token right) => (left, right) = (right, left);
	}
}