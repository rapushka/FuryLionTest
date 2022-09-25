using System.Collections.Generic;
using Code.Extensions;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment.Gravity.Movers
{
	public class VerticallyMover : IDirectionMover
	{
		private Token[,] _tokens;

		public Token[,] Move(Token[,] tokens, Dictionary<Vector2Int, Vector3> positions)
		{
			_tokens = tokens;
			
			positions.ForEach(FallTokenVertically);
				
			return _tokens;
		}
		
		private void FallTokenVertically(KeyValuePair<Vector2Int, Vector3> pair)
		{
			var position = pair.Key;
			var direction = pair.Value;
			for (var y = position.y; y > 0 && BellowIsEmpty(_tokens, position.x, y); y--)
			{
				_tokens[position.x, y].transform.Translate(direction);

				Swap(ref _tokens[position.x, y], ref _tokens[position.x, y - 1]);
			}
		}

		private static bool BellowIsEmpty(Token[,] tokens, int x, int y) => tokens[x, y - 1] == false;

		private void Swap(ref Token left, ref Token right) => (left, right) = (right, left);
	}
}