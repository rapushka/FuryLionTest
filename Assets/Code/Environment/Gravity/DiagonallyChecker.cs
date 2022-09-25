using System.Collections.Generic;
using System.Linq;
using Code.Environment.Gravity.Interfaces;
using Code.Extensions;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment.Gravity
{
	public class DiagonallyChecker : IDirectionChecker
	{
		private readonly Dictionary<Vector2Int, Vector3> _result;

		private Token[,] _tokens;

		public DiagonallyChecker()
		{
			_result = new Dictionary<Vector2Int, Vector3>();
		}

		public bool HasPrecedentTokens(Token[,] tokens, out Dictionary<Vector2Int, Vector3> result)
		{
			_result.Clear();
			_tokens = tokens;

			result = FillResult(_tokens);
			return _result.Any();
		}

		private Dictionary<Vector2Int, Vector3> FillResult(Token[,] tokens)
		{
			var marked = tokens.FirstOrDefault(MarkDiagonallyToken)
			                   ?.transform.position.ToVectorInt();

			if (marked != null)
			{
				AddToResult(marked.Value);
			}

			return _result;
		}

		private void AddToResult(Vector2Int entry) => _result.Add(entry, GetDirection(entry));

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