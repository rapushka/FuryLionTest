using System.Collections.Generic;
using Code.Extensions;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment
{
	public class Gravity
	{
		private readonly List<Vector2Int> _willFallVertically;
		private readonly Dictionary<Vector2Int, Direction> _willFallDiagonally;

		private Token[,] _tokens;

		public Gravity()
			=> (_willFallVertically, _willFallDiagonally)
				= (new List<Vector2Int>(), new Dictionary<Vector2Int, Direction>());

		public Token[,] Apply(Token[,] tokens)
		{
			_tokens = (Token[,])tokens.Clone();

			MarkToFallingVertically();
			FallVertically();
			MarkToFallDiagonally();
			_willFallDiagonally.ForEach((p) => Debug.Log($"{p.Key} → {p.Value}"));

			return _tokens;
		}

		private void FallVertically()
		{
			foreach (var indexes in _willFallVertically)
			{
				var currentY = indexes.y;
				while (currentY > 0
				       && _tokens[indexes.x, currentY - 1] == false)
				{
					_tokens[indexes.x, currentY].transform.Translate(Vector3.down);

					Swap(ref _tokens[indexes.x, currentY], ref _tokens[indexes.x, currentY - 1]);
					currentY--;
				}
			}

			_willFallVertically.Clear();
		}

		private void Swap(ref Token left, ref Token right) => (left, right) = (right, left);

		private void MarkToFallingVertically() => _tokens.DoubleFor(MarkVerticallyToken);

		private void MarkVerticallyToken(Token token, int x, int y)
		{
			if (token == true
			    && token.ApplyGravity
			    && TokenBellowIsEmpty(x, y)
			    && _willFallVertically.Contains(new Vector2Int(x, y)) == false)
			{
				MarkWithAboveTokens(x, y);
			}
		}

		private void MarkToFallDiagonally()
		{
			_tokens.DoubleFor(MarkDiagonallyToken);
		}

		private void MarkDiagonallyToken(Token token, int x, int y)
		{
			if (token == false)
			{
				return;
			}

			var tokenPosition = token.transform.position.ToVectorInt();
			if (token.ApplyGravity == false
			    || _willFallDiagonally.ContainsKey(tokenPosition))
			{
				return;
			}

			var direction = TokenOnDiagonalIsEmpty(x, y);
			if (direction != Direction.None)
			{
				_willFallDiagonally.Add(tokenPosition, direction);
			}
		}

		private Direction TokenOnDiagonalIsEmpty(int x, int y)
			=> IsOutOfBounce(x, y) ? Direction.None
				: IsCanFallDiagonallyLeft(x, y) ? Direction.Left
				: IsCanFallDiagonallyRight(x, y) ? Direction.Right
				: Direction.None;

		private bool IsOutOfBounce(int x, int y)
			=> x < 0
			   || x == _tokens.GetLength(0)
			   || y <= 0
			   || y == _tokens.GetLength(1);

		private bool IsCanFallDiagonallyLeft(int x, int y) => x > 0 && _tokens[x - 1, y - 1] == false;

		private bool IsCanFallDiagonallyRight(int x, int y)
			=> x < _tokens.GetLength(0) - 1
			   && _tokens[x + 1, y - 1] == false;

		private bool TokenBellowIsEmpty(int x, int y) => y != 0 || _tokens[x, y - 1] == false;

		private void MarkWithAboveTokens(int startX, int startY)
		{
			for (var y = startY; Continue(startX, y); y++)
			{
				_willFallVertically.Add(new Vector2Int(startX, y));
			}
		}

		private bool Continue(int x, int y)
			=> x < _tokens.GetLength(0)
			   && (_tokens[x, y]
			       && _tokens[x, y].ApplyGravity);
	}
}