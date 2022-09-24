using System.Linq;
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
			if (_vertical.HasPrecedent(_tokens, out var positions))
			{
				_tokens = _vertical.Move(_tokens, positions);
			}
			else
			{
				_mayBePrecedents = false;
			}
		}

		private void DiagonallyCheck()
		{
			if (_mayBePrecedents == false
			    && _diagonal.HasPrecedent(_tokens, out var position, out var direction)
			    && position is not null)
			{
				MoveDiagonally(position.First(), direction);
			}
		}

		private void MoveDiagonally(Vector2Int position, Vector3 direction)
		{
			_tokens = _diagonal.Move(_tokens, position, direction);
			_mayBePrecedents = true;
		}
	}
}