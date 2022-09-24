using System.Collections.Generic;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment
{
	public class VerticallyMover
	{
		public Token[,] Move(Token[,] tokens, IEnumerable<Vector2Int> positions)
		{
			foreach (var entry in positions)
			{
				FallTokenVertically(tokens, entry);
			}

			return tokens;
		}

		private void FallTokenVertically(Token[,] tokens, Vector2Int position)
		{
			for (var y = position.y; y > 0 && BellowIsEmpty(tokens, position.x, y); y--)
			{
				tokens[position.x, y].transform.Translate(Vector3.down);

				Swap(ref tokens[position.x, y], ref tokens[position.x, y - 1]);
			}
		}

		private static bool BellowIsEmpty(Token[,] tokens, int x, int y) => tokens[x, y - 1] == false;

		private void Swap(ref Token left, ref Token right) => (left, right) = (right, left);
	}
}