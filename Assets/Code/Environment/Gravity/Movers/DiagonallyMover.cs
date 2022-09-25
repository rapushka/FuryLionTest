using System.Collections.Generic;
using Code.Extensions;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment.Gravity.Movers
{
	public class DiagonallyMover : IDirectionMover
	{
		private Token[,] _tokens;

		public Token[,] Move(Token[,] tokens, Dictionary<Vector2Int, Vector3> positions)
		{
			_tokens = tokens;
			
			positions.ForEach(FallTokenDiagonally);
			
			return _tokens;
		}
		
		private void FallTokenDiagonally(KeyValuePair<Vector2Int, Vector3> pair)
		{
			var position = pair.Key;
			var direction = pair.Value;
			
			_tokens[position.x, position.y].transform.Translate(Vector3.down + direction);

			Swap(ref _tokens[position.x, position.y], ref _tokens[position.x + (int)direction.x, position.y - 1]);
		}

		private void Swap(ref Token left, ref Token right) => (left, right) = (right, left);
	}
}