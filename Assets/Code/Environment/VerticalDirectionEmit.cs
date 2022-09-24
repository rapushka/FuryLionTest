using System.Collections.Generic;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment
{
	public class VerticalDirectionEmit
	{
		private readonly VerticallyChecker _checker;
		private readonly VerticallyMover _mover;

		public VerticalDirectionEmit()
		{
			_checker = new VerticallyChecker();
			_mover = new VerticallyMover();
		}

		public bool HasPrecedent(Token[,] tokens, out IEnumerable<Vector2Int> result) 
			=> _checker.HasPrecedentTokens(tokens, out result);

		public Token[,] Move(Token[,] tokens, IEnumerable<Vector2Int> positions) => _mover.Move(tokens, positions);
	}
}