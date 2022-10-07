using System.Collections.Generic;
using Code.Environment.GravityBehaviour.Checkers;
using Code.Environment.GravityBehaviour.Movers;
using Code.Gameplay.Tokens;
using UnityEngine;

namespace Code.Environment.GravityBehaviour
{
	public class DirectionEmit
	{
		private readonly TokensViewsMover _tokensViewsMover;
		private readonly IDirectionChecker _checker;
		private readonly IDirectionMover _mover;

		public DirectionEmit(IDirectionChecker checker, IDirectionMover mover, TokensViewsMover tokensViewsMover)
		{
			_tokensViewsMover = tokensViewsMover;
			(_checker, _mover) = (checker, mover);
		}

		public bool HasContender(Token[,] tokens, out Dictionary<Vector2Int, Vector3> result)
			=> _checker.HasContenderTokens(tokens, out result);

		public Token[,] Move(Token[,] tokens, Dictionary<Vector2Int, Vector3> directionsForIndexes)
			=> _mover.Move(tokens, directionsForIndexes, _tokensViewsMover);
	}
}