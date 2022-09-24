using System.Collections.Generic;
using Code.Environment.Gravity.Interfaces;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment.Gravity
{
	public class VerticalDirectionEmit
	{
		private readonly IDirectionChecker _checker;
		private readonly VerticallyMover _mover;

		public VerticalDirectionEmit()
		{
			_checker = new VerticallyChecker();
			_mover = new VerticallyMover();
		}

		public bool HasPrecedent(Token[,] tokens, out IEnumerable<Vector2Int> result, out Vector3 direction) 
			=> _checker.HasPrecedentTokens(tokens, out result, out direction);

		public Token[,] Move(Token[,] tokens, IEnumerable<Vector2Int> positions) => _mover.Move(tokens, positions);
	}
}