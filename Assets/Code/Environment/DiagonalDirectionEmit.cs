using System.Collections.Generic;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment
{
	public class DiagonalDirectionEmit
	{
		private readonly DiagonallyChecker _checker;
		private readonly DiagonallyMover _mover;

		public DiagonalDirectionEmit()
		{
			_checker = new DiagonallyChecker();
			_mover = new DiagonallyMover();
		}

		public bool HasPrecedent(Token[,] tokens, out Vector2Int? result, out Vector3 direction) 
			=> _checker.HasPrecedentToken(tokens, out result, out direction);

		public Token[,] Move(Token[,] tokens, Vector2Int position, Vector3 direction) 
			=> _mover.Move(tokens, position, direction);
	}
}