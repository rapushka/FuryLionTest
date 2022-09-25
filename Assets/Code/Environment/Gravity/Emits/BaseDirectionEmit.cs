using System.Collections.Generic;
using Code.Environment.Gravity.Checkers;
using Code.Environment.Gravity.Movers;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment.Gravity.Emits
{
	public class BaseDirectionEmit
	{
		private readonly IDirectionChecker _checker;
		private readonly IDirectionMover _mover;

		public BaseDirectionEmit(IDirectionChecker checker, IDirectionMover mover)
			=> (_checker, _mover) = (checker, mover);

		public bool HasPrecedent(Token[,] tokens, out Dictionary<Vector2Int, Vector3> result)
			=> _checker.HasPrecedentTokens(tokens, out result);

		public Token[,] Move(Token[,] tokens, Dictionary<Vector2Int, Vector3> positions)
			=> _mover.Move(tokens, positions);
	}
}