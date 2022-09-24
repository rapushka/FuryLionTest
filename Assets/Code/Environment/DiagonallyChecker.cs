using Code.Extensions;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment
{
	public class DiagonallyChecker
	{
		private Token[,] _tokens;
		private Vector3 _direction;

		public bool HasTokenToMoveDiagonally(Token[,] tokens, out Vector2Int? result, out Vector3 direction)
		{
			_tokens = tokens;

			result = _tokens.FirstOrDefaultFromEnd(MarkDiagonallyToken)
			                ?.transform.position.ToVectorInt();

			direction = _direction;
			return result is not null;
		}

		private bool MarkDiagonallyToken(Token token, int x, int y)
			=> token == true
			   && token.ApplyGravity
			   && TokenDiagonallyBellowIsEmpty(x, y);

		private bool TokenDiagonallyBellowIsEmpty(int x, int y)
			=> (_direction = GetDirection(x, y)) != Vector3.zero;

		private Vector3 GetDirection(int x, int y)
			=> IsAtBottomBorder(y) ? Vector3.zero
				: CanMoveBottomLeft(x, y) ? Vector3.left
				: CanMoveBottomRight(x, y) ? Vector3.right
				: Vector3.zero;

		private static bool IsAtBottomBorder(int y) => y <= 0;

		private bool CanMoveBottomLeft(int x, int y)
			=> IsNotAtLeftBorder(x)
			   && IsEmptyAtBottomLeft(x, y);

		private bool CanMoveBottomRight(int x, int y)
			=> IsNotOnRightBorder(x)
			   && IsEmptyAtBottomRight(x, y);

		private static bool IsNotAtLeftBorder(int x) => x > 0;

		private bool IsEmptyAtBottomLeft(int x, int y) => _tokens[x - 1, y - 1] == false;

		private bool IsNotOnRightBorder(int x) => x < _tokens.GetLength(0) - 1;

		private bool IsEmptyAtBottomRight(int x, int y) => _tokens[x + 1, y - 1] == false;
	}
}