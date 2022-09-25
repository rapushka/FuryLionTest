using System.Collections.Generic;
using Code.Extensions;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment.Gravity.Movers
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

		protected static void Swap(ref Token left, ref Token right) => (left, right) = (right, left);

		protected abstract void FallTokenAtDirection(KeyValuePair<Vector2Int, Vector3> obj);
	}
}