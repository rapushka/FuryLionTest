using System.Collections.Generic;
using Code.Environment.Gravity.Interfaces;
using Code.Extensions;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment.Gravity
{
	public class VerticallyMover : IDirectionMover
	{
		private Token[,] _tokens;
		private Vector3 _direction;

		public Token[,] Move(Token[,] tokens, IEnumerable<Vector2Int> positions, Vector3 direction)
		{
			_direction = direction;
			_tokens = tokens;
			
			positions.ForEach(FallTokenVertically);
				
			return _tokens;
		}
		
		private void FallTokenVertically(Vector2Int position)
		{
			for (var y = position.y; y > 0 && BellowIsEmpty(_tokens, position.x, y); y--)
			{
				_tokens[position.x, y].transform.Translate(_direction);

				Swap(ref _tokens[position.x, y], ref _tokens[position.x, y - 1]);
			}
		}

		private static bool BellowIsEmpty(Token[,] tokens, int x, int y) => tokens[x, y - 1] == false;

		private void Swap(ref Token left, ref Token right) => (left, right) = (right, left);
	}
}