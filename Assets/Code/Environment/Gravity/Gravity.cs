using System.Collections.Generic;
using Code.Environment.Gravity.Emits;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment.Gravity
{
	public class Gravity
	{
		private readonly VerticalDirectionEmit _vertical;
		private readonly DiagonalDirectionEmit _diagonal;

		private Token[,] _tokens;
		private bool _mayBePrecedents;

		public Gravity()
		{
			_vertical = new VerticalDirectionEmit();
			_diagonal = new DiagonalDirectionEmit();
		}

		public Token[,] Apply(Token[,] tokens)
		{
			_tokens = tokens;
			_mayBePrecedents = true;

			while (_mayBePrecedents)
			{
				VerticallyCheck();
				DiagonallyCheck();
			}

			return _tokens;
		}

		private void VerticallyCheck()
		{
			if (_vertical.HasPrecedent(_tokens, out var positions, out var direction))
			{
				_tokens = _vertical.Move(_tokens, positions, direction);
			}
			else
			{
				_mayBePrecedents = false;
			}
		}

		private void DiagonallyCheck()
		{
			if (_mayBePrecedents == false
			    && _diagonal.HasPrecedent(_tokens, out var positions, out var direction)
			    && positions is not null)
			{
				MoveDiagonally(positions, direction);
			}
		}

		private void MoveDiagonally(IEnumerable<Vector2Int> position, Vector3 direction)
		{
			_tokens = _diagonal.Move(_tokens, position, direction);
			_mayBePrecedents = true;
		}
	}
}