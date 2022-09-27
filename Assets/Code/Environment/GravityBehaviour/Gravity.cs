using System.Collections.Generic;
using Code.Environment.GravityBehaviour.Checkers;
using Code.Environment.GravityBehaviour.Emits;
using Code.Environment.GravityBehaviour.Movers;
using Code.Gameplay;
using UnityEngine;
using Zenject;

namespace Code.Environment.GravityBehaviour
{
	public class Gravity
	{
		private readonly BaseDirectionEmit _vertical;
		private readonly BaseDirectionEmit _diagonal;

		private Token[,] _tokens;
		private bool _mayBePrecedents;

		[Inject]
		public Gravity()
		{
			_vertical = new BaseDirectionEmit(new VerticallyChecker(), new VerticallyMover());
			_diagonal = new BaseDirectionEmit(new DiagonallyChecker(), new DiagonallyMover());
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
			    && _diagonal.HasPrecedent(_tokens, out var positions)
			    && positions is not null)
			{
				MoveDiagonally(positions);
			}
		}

		private void MoveDiagonally(Dictionary<Vector2Int, Vector3> positions)
		{
			_tokens = _diagonal.Move(_tokens, positions);
			_mayBePrecedents = true;
		}
	}
}