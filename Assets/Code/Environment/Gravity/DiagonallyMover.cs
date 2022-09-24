using System.Collections.Generic;
using Code.Environment.Gravity.Interfaces;
using Code.Extensions;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment.Gravity
{
	public class DiagonallyMover : IDirectionMover
	{
		private Token[,] _tokens;
		private Vector3 _direction;

		public Token[,] Move(Token[,] tokens, IEnumerable<Vector2Int> positions, Vector3 direction)
		{
			_tokens = tokens;
			_direction = direction;
			
			positions.ForEach(FallTokenDiagonally);
			
			return _tokens;
		}
		
		private void FallTokenDiagonally(Vector2Int position)
		{
			_tokens[position.x, position.y].transform.Translate(Vector3.down + _direction);

			Swap(ref _tokens[position.x, position.y], ref _tokens[position.x + (int)_direction.x, position.y - 1]);
		}

		private void Swap(ref Token left, ref Token right) => (left, right) = (right, left);
	}
}