using System.Collections.Generic;
using System.Linq;
using Code.Extensions;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment
{
	public class VerticallyChecker
	{
		private readonly List<Vector2Int> _result;

		private Token[,] _tokens;

		public VerticallyChecker()
		{
			_result = new List<Vector2Int>();
		}

		public bool HasTokensToMoveVertically(Token[,] tokens, out IEnumerable<Vector2Int> result)
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
			    && _result.Contains(new Vector2Int(x, y)) == false)
			{
				_result.Add(new Vector2Int(x, y));
			}
		}

		private bool TokenBellowIsEmpty(int x, int y) => y > 0 && _tokens[x, y - 1] == false;
	}
}