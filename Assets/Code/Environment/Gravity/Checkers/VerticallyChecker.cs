using System.Collections.Generic;
using System.Linq;
using Code.Extensions;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment.Gravity.Checkers
{
	public class VerticallyChecker : IDirectionChecker
	{
		private Token[,] _tokens;

		public bool HasPrecedentTokens(Token[,] tokens, out Dictionary<Vector2Int, Vector3> result)
		{
			_tokens = tokens;

			result = FillResults(_tokens);
			return result.Any();
		}

		private Dictionary<Vector2Int, Vector3> FillResults(Token[,] tokens)
			=> tokens.Where(MarkVerticallyToken)
			         .Select((t) => t.transform.position.ToVectorInt())
			         .ToDictionary((p) => p, GetDirection);

		private Vector3 GetDirection(Vector2Int position) => GetDirection(position.x, position.y);

		private bool MarkVerticallyToken(Token token, int x, int y)
			=> token == true
			   && token.ApplyGravity
			   && TokenBellowIsEmpty(x, y);

		private bool TokenBellowIsEmpty(int x, int y) => y > 0 && _tokens[x, y - 1] == false;

		private Vector3 GetDirection(int x, int y) => Vector3.down;
	}
}