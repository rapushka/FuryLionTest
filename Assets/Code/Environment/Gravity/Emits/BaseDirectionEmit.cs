using System.Collections.Generic;
using Code.Environment.Gravity.Interfaces;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment.Gravity.Emits
{
	public abstract class BaseDirectionEmit
	{
		private readonly IDirectionChecker _checker;
		private readonly IDirectionMover _mover;

		protected BaseDirectionEmit(IDirectionChecker checker, IDirectionMover mover)
			=> (_checker, _mover) = (checker, mover);

		public bool HasPrecedent(Token[,] tokens, out IEnumerable<Vector2Int> result, out Vector3 direction)
			=> _checker.HasPrecedentTokens(tokens, out result, out direction);

		public Token[,] Move(Token[,] tokens, IEnumerable<Vector2Int> positions, Vector3 direction)
			=> _mover.Move(tokens, positions, direction);
	}
}