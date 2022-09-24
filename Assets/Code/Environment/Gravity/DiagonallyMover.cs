using Code.Gameplay;
using UnityEngine;

namespace Code.Environment.Gravity
{
	public class DiagonallyMover
	{
		public Token[,] Move(Token[,] tokens, Vector2Int position, Vector3 direction)
		{
			FallTokenDiagonally(tokens, position, direction);
			return tokens;
		}

		private void FallTokenDiagonally(Token[,] tokens, Vector2Int indexes, Vector3 direction)
		{
			tokens[indexes.x, indexes.y].transform.Translate(Vector3.down + direction);

			Swap(ref tokens[indexes.x, indexes.y], ref tokens[indexes.x + (int)direction.x, indexes.y - 1]);
		}

		private void Swap(ref Token left, ref Token right) => (left, right) = (right, left);
	}
}