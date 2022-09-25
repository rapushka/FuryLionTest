using System.Collections.Generic;
using System.Linq;
using Code.Environment.Gravity.Interfaces;
using Code.Extensions;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment.Gravity
{
	public class VerticallyChecker : IDirectionChecker
	{
		private readonly Dictionary<Vector2Int, Vector3> _result;

		private Token[,] _tokens;

		public VerticallyChecker()
		{
			_result = new Dictionary<Vector2Int, Vector3>();
		}

		public bool HasPrecedentTokens(Token[,] tokens, out Dictionary<Vector2Int, Vector3> result)
		{
			_result.Clear();
			_tokens = tokens;

			_tokens.DoubleForReversed(MarkVerticallyToken);

			result = _result;
			return result.Any();
		}
		
		private void MarkVerticallyToken(Token token, int x, int y)
		{
			if (token == true
			    && token.ApplyGravity
			    && TokenBellowIsEmpty(x, y)
			    && _result.ContainsKey(new Vector2Int(x, y)) == false)
			{
				MarkWithAboveTokens(x, y);
			}
		}

		private bool TokenBellowIsEmpty(int x, int y) => y > 0 && _tokens[x, y - 1] == false;

		private void MarkWithAboveTokens(int startX, int startY)
		{
			for (var y = startY; VerticalLineNotEnded(startX, y); y++)
			{
				_result.Add(new Vector2Int(startX, y), Vector3.down);
			}
		}

		private bool VerticalLineNotEnded(int x, int y)
			=> y < _tokens.GetLength(1)
			   && (_tokens[x, y] && _tokens[x, y].ApplyGravity);
	}
}