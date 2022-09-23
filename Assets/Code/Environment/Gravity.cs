using System.Collections.Generic;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment
{
	public class Gravity
	{
		private readonly List<Vector2Int> _willFall;

		private Token[,] _tokens;

		public Gravity() => _willFall = new List<Vector2Int>();

		public Token[,] Apply(Token[,] tokens)
		{
			_tokens = (Token[,])tokens.Clone();

			MarkToFalling();
			FallVertically();

			return _tokens;
		}

		private void FallVertically()
		{
			foreach (var indexes in _willFall)
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

			_willFall.Clear();
		}

		private void Swap(ref Token left, ref Token right) => (left, right) = (right, left);

		private void MarkToFalling()
		{
			for (var x = _tokens.GetLength(0) - 1; x >= 0; x--)
			{
				for (var y = _tokens.GetLength(1) - 1; y >= 0; y--)
				{
					var token = _tokens[x, y];

					if (token == false
					    || token.ApplyGravity == false
					    || TokenBellowIsNotEmpty(x, y)
					    || _willFall.Contains(new Vector2Int(x, y)))
					{
						continue;
					}

					MarkWithAboveTokens(x, y);
				}
			}
		}

		private bool TokenBellowIsNotEmpty(int x, int y) => y == 0 || _tokens[x, y - 1] == true;

		private void MarkWithAboveTokens(int startX, int startY)
		{
			for (var y = startY; Continue(startX, y); y++)
			{
				_willFall.Add(new Vector2Int(startX, y));
			}
		}

		private bool Continue(int x, int y)
			=> x < _tokens.GetLength(0)
			   && (_tokens[x, y]
			       && _tokens[x, y].ApplyGravity);
	}
}