using System.Collections.Generic;
using Code.Environment.Gravity.Interfaces;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment.Gravity.Emits
{
	public class DiagonalDirectionEmit
	{
		private readonly IDirectionChecker _checker;
		private readonly IDirectionMover _mover;

		public DiagonalDirectionEmit()
		{
			_checker = new DiagonallyChecker();
			_mover = new DiagonallyMover();
		}

		public bool HasPrecedent(Token[,] tokens, out IEnumerable<Vector2Int> result, out Vector3 direction) 
			=> _checker.HasPrecedentTokens(tokens, out result, out direction);

		public Token[,] Move(Token[,] tokens, IEnumerable<Vector2Int> positions, Vector3 direction) 
			=> _mover.Move(tokens, positions, direction);
	}
}