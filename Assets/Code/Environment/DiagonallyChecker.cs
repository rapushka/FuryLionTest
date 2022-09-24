using Code.Extensions;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment
{
	public class DiagonallyChecker
	{
		private Token[,] _tokens;
		private Direction _direction;

		public bool HasTokenToMoveDiagonally(Token[,] tokens, out Vector2Int? result, out Direction direction)
		{
			_tokens = tokens;

			_direction = Direction.None;
			result = _tokens.FirstOrDefault(MarkDiagonallyToken)
			                ?.transform.position.ToVectorInt();

			direction = _direction;
			return result is not null;
		}

		private bool MarkDiagonallyToken(Token token, int x, int y)
			=> token == true
			   && token.ApplyGravity
			   && TokenDiagonallyBellowIsEmpty(x, y);

		private bool TokenDiagonallyBellowIsEmpty(int x, int y) 
			=> (_direction = GetDirection(x, y)) != Direction.None;

		private Direction GetDirection(int x, int y)
			=> IsAtBottomBorder(y) ? Direction.None
				: CanMoveBottomLeft(x, y) ? Direction.Left
				: CanMoveBottomRight(x, y) ? Direction.Right
				: Direction.None;

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