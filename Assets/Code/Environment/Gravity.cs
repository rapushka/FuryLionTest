using System.Collections.Generic;
using System.Linq;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment
{
	public class Gravity
	{
		private Token[,] _tokens;
		private List<Vector2Int> _willFall;

		public Gravity()
		{
			_willFall = new List<Vector2Int>();
		}

		public Token[,] Apply(Token[,] tokens)
		{
			_tokens = (Token[,])tokens.Clone();

			MarkToFalling();
			FallVertically();

			return _tokens;
		}

		private void FallVertically()
		{
			_willFall.ForEach((i) => Debug.Log(i));
			foreach (var indexes in _willFall)
			{
				_tokens[indexes.x, indexes.y]
					.transform.Translate(GetTokenBellow(indexes.x, indexes.y).transform.position);
			}
			
			_willFall.ForEach((i) => Debug.Log(i));
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
					    || GetTokenBellow(x, y)
					    || _willFall.Contains(new Vector2Int(x, y)))
					{
						continue;
					}

					MarkWithAboveTokens(x, y);
				}
			}
		}

		private Token GetTokenBellow(int x, int y) => _tokens[x + 1, y];

		private void MarkWithAboveTokens(int startX, int startY)
		{
			for (var x = startX; Continue(x, startY); x--)
			{
				_willFall.Add(new Vector2Int(x, startY));
			}
		}

		private bool Continue(int x, int y)
			=> x < _tokens.GetLength(0)
			   && (_tokens[x, y]
			       && _tokens[x, y].ApplyGravity);
	}
}