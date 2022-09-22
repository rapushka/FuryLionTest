using System.Collections.Generic;
using System.Linq;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment
{
	public class Gravity
	{
		private Token[,] _tokens;
		private List<Vector2> _willFall;

		public Gravity()
		{
			_willFall = new List<Vector2>();
		}

		public Token[,] Apply(Token[,] tokens)
		{
			_tokens = (Token[,])tokens.Clone();
			MarkToFalling();

			return _tokens;
		}

		private void MarkToFalling()
		{
			for (var x = _tokens.GetLength(0) - 1; x >= 0; x--)
			{
				for (var y = _tokens.GetLength(1) - 1; y >= 0; y--)
				{
					var token = _tokens[x, y];

					if (token == false
					    || token.ApplyGravity == false
					    || _tokens[x + 1, y]
					    || _willFall.Contains(new Vector2(x, y)))
					{
						continue;
					}

					MarkAlsoAbove(x, y);
				}
			}
		}

		private void MarkAlsoAbove(int startX, int startY)
		{
			for (var x = startX; Continue(x, startY); x--)
			{
				_willFall.Add(new Vector2(x, startY));
			}
		}

		private bool Continue(int x, int y)
			=> x < _tokens.GetLength(0)
			   && (_tokens[x, y]
			       && _tokens[x, y].ApplyGravity);
	}
}