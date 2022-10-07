using System.Collections.Generic;
using Code.Environment.GravityBehaviour.Checkers;
using Code.Environment.GravityBehaviour.Movers;
using Code.Gameplay.Tokens;
using UnityEngine;
using Zenject;

namespace Code.Environment.GravityBehaviour
{
	public class Gravity
	{
		private readonly DirectionEmit _vertical;
		private readonly DirectionEmit _diagonal;

		private Token[,] _tokens;
		private bool _mayBeContender;

		[Inject]
		public Gravity(TokensViewsMover tokensViewsMover)
		{
			_vertical = new DirectionEmit(new VerticallyChecker(), new VerticallyMover(), tokensViewsMover);
			_diagonal = new DirectionEmit(new DiagonallyChecker(), new DiagonallyMover(), tokensViewsMover);
		}

		public Token[,] Apply(Token[,] tokens)
		{
			_tokens = tokens;
			_mayBeContender = true;

			while (_mayBeContender)
			{
				VerticallyCheck();
				DiagonallyCheck();
			}

			return _tokens;
		}

		private void VerticallyCheck()
		{
			if (_vertical.HasContender(_tokens, out var positions))
			{
				_tokens = _vertical.Move(_tokens, positions);
			}
			else
			{
				_mayBeContender = false;
			}
		}

		private void DiagonallyCheck()
		{
			if (_mayBeContender == false
			    && _diagonal.HasContender(_tokens, out var positions)
			    && positions is not null)
			{
				MoveDiagonally(positions);
			}
		}

		private void MoveDiagonally(Dictionary<Vector2Int, Vector3> positions)
		{
			_tokens = _diagonal.Move(_tokens, positions);
			_mayBeContender = true;
		}
	}
}