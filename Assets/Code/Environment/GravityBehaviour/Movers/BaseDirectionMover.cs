using System.Collections.Generic;
using Code.Extensions;
using Code.Gameplay.Tokens;
using UnityEngine;

namespace Code.Environment.GravityBehaviour.Movers
{
	public abstract class BaseDirectionMover : IDirectionMover
	{
		protected Token[,] Tokens { get; private set; }

		public Token[,] Move(Token[,] tokens, Dictionary<Vector2Int, Vector3> positions)
		{
			Tokens = tokens;
			positions.ForEach(FallTokenAtDirection);
			return Tokens;
		}

		protected abstract void FallTokenAtDirection(KeyValuePair<Vector2Int, Vector3> obj);

		protected void MoveToken(int x, int y, Vector3 to)
		{
			Tokens[x, y].transform.Translate(to);
			Swap(ref Tokens[x, y], ref Tokens[x + (int)to.x, y + (int)to.y]);
		}

		private static void Swap(ref Token left, ref Token right) => (left, right) = (right, left);
	}
}