using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Tokens;
using UnityEngine;

namespace Code.Environment.GravityBehaviour.Checkers
{
	public abstract class BaseDirectionChecker : IDirectionChecker
	{
		protected Token[,] Tokens { get; private set; }

		public bool HasPrecedentTokens(Token[,] tokens, out Dictionary<Vector2Int, Vector3> result)
		{
			Tokens = tokens;

			result = FillResults(Tokens);
			return result.Any();
		}

		protected bool TokenIsPrecedent(Token token, int x, int y)
			=> token == true
			   && token.ApplyGravity
			   && TokenOnDirectionIsEmpty(x, y);

		protected abstract Dictionary<Vector2Int, Vector3> FillResults(Token[,] tokens);

		protected Vector3 GetDirection(Vector2Int position) => GetDirection(position.x, position.y);
		
		protected abstract Vector3 GetDirection(int x, int y);

		protected abstract bool TokenOnDirectionIsEmpty(int x, int y);
	}
}