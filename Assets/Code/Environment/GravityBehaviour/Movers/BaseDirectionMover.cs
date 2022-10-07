using System.Collections.Generic;
using Code.Extensions;
using Code.Gameplay.Tokens;
using UnityEngine;
using Zenject;

namespace Code.Environment.GravityBehaviour.Movers
{
	public abstract class BaseDirectionMover : IDirectionMover
	{
		private SignalBus _signalBus;
		private TokensViewsMover _mover;
		protected Token[,] Tokens { get; private set; }

		public Token[,] Move(Token[,] tokens, Dictionary<Vector2Int, Vector3> directionsForIndexes, TokensViewsMover mover)
		{
			_mover = mover;
			Tokens = tokens;
			directionsForIndexes.ForEach(FallTokenAtDirection);
			return Tokens;
		}

		protected abstract void FallTokenAtDirection(KeyValuePair<Vector2Int, Vector3> pair);

		protected void MoveToken(int x, int y, Vector3 to)
		{
			_mover.MoveView(Tokens[x, y], to);
			Swap(ref Tokens[x, y], ref Tokens[x + (int)to.x, y + (int)to.y]);
		}

		private static void Swap(ref Token left, ref Token right) => (left, right) = (right, left);
	}
}