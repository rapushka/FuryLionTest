using System.Collections.Generic;
using Code.Environment.GravityBehaviour.Checkers;
using Code.Environment.GravityBehaviour.Movers;
using Code.Gameplay.Tokens;
using UnityEngine;

namespace Code.Environment.GravityBehaviour
{
	public class DirectionEmit
	{
		private readonly IDirectionChecker _checker;
		private readonly IDirectionMover _mover;

		public DirectionEmit(IDirectionChecker checker, IDirectionMover mover)
			=> (_checker, _mover) = (checker, mover);

		public bool HasPrecedent(Token[,] tokens, out Dictionary<Vector2Int, Vector3> result)
			=> _checker.HasPrecedentTokens(tokens, out result);

		public Token[,] Move(Token[,] tokens, Dictionary<Vector2Int, Vector3> positions)
			=> _mover.Move(tokens, positions);
	}
}