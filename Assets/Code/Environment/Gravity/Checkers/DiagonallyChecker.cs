using System.Collections.Generic;
using System.Linq;
using Code.Extensions;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment.Gravity.Checkers
{
	public class DiagonallyChecker : IDirectionChecker
	{
		private Token[,] _tokens;

		public bool HasPrecedentTokens(Token[,] tokens, out Dictionary<Vector2Int, Vector3> result)
		{
			_tokens = tokens;

			result = FillResults(_tokens);
			return result.Any();
		}

		private Dictionary<Vector2Int, Vector3> FillResults(Token[,] tokens)
			=> tokens.FirstOrDefault(MarkDiagonallyToken)
			         ?.transform.position.ToVectorInt()
			         .ToDictionary((p) => p, GetDirection)
			         ?? new Dictionary<Vector2Int, Vector3>();

		private Vector3 GetDirection(Vector2Int position) => GetDirection(position.x, position.y);

		private bool MarkDiagonallyToken(Token token, int x, int y)
			=> token == true
			   && token.ApplyGravity
			   && TokenDiagonallyBellowIsEmpty(x, y);

		private bool TokenDiagonallyBellowIsEmpty(int x, int y)
			=> GetDirection(x, y) != Vector3.zero;

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