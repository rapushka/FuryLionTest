using System.Collections.Generic;
using Code.Extensions;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment.Gravity.Checkers
{
	public class DiagonallyChecker : BaseDirectionChecker
	{
		protected override Dictionary<Vector2Int, Vector3> FillResults(Token[,] tokens)
			=> tokens.FirstOrDefault(TokenIsPrecedent)
			         ?.transform.position.ToVectorInt()
			         .ToDictionary((p) => p, GetDirection)
			         ?? new Dictionary<Vector2Int, Vector3>();

		protected override Vector3 GetDirection(int x, int y)
			=> IsOnBottomBorder(y) ? Vector3.zero
				: CanMoveBottomLeft(x, y) ? Vector3.left
				: CanMoveBottomRight(x, y) ? Vector3.right
				: Vector3.zero;

		protected override bool TokenOnDirectionIsEmpty(int x, int y)
			=> GetDirection(x, y) != Vector3.zero;

		private static bool IsOnBottomBorder(int y) => y <= 0;

		private bool CanMoveBottomLeft(int x, int y)
			=> IsNotOnLeftBorder(x)
			   && IsEmptyOnBottomLeft(x, y);

		private bool CanMoveBottomRight(int x, int y)
			=> IsNotOnRightBorder(x)
			   && IsEmptyOnBottomRight(x, y);

		private static bool IsNotOnLeftBorder(int x) => x > 0;

		private bool IsEmptyOnBottomLeft(int x, int y) => Tokens[x - 1, y - 1] == false;

		private bool IsNotOnRightBorder(int x) => x < Tokens.GetLength(0) - 1;

		private bool IsEmptyOnBottomRight(int x, int y) => Tokens[x + 1, y - 1] == false;
	}
}