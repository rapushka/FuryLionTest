using Code.Environment.GravityBehaviour.Checkers;
using Code.Environment.GravityBehaviour.Movers;
using Code.Gameplay.Tokens;
using Zenject;

namespace Code.Environment.GravityBehaviour
{
	public class Gravity
	{
		private readonly DirectionEmit _vertical;
		private readonly DirectionEmit _diagonal;

		private Token[,] _tokens;

		[Inject]
		public Gravity(TokensViewsMover tokensViewsMover, Field field)
		{
			_vertical = new DirectionEmit(new VerticallyChecker(field), new VerticallyMover(), tokensViewsMover);
			_diagonal = new DirectionEmit(new DiagonallyChecker(field), new DiagonallyMover(), tokensViewsMover);
		}

		public Token[,] Apply(Token[,] tokens)
		{
			_tokens = tokens;
			var mayBeContender = true;

			while (mayBeContender)
			{
				mayBeContender = VerticallyCheck();
				mayBeContender = DiagonallyCheck(mayBeContender);
			}

			return _tokens;
		}

		private bool VerticallyCheck()
		{
			var hasContender = _vertical.HasContender(_tokens, out var directionsForIndexes);
			if (hasContender)
			{
				_tokens = _vertical.Move(_tokens, directionsForIndexes);
			}

			return hasContender;
		}

		private bool DiagonallyCheck(bool mayBeContender)
		{
			if (mayBeContender)
			{
				return true;
			}

			var hasContender = _diagonal.HasContender(_tokens, out var positions)
			        && positions is not null;
			if (hasContender)
			{
				_tokens = _diagonal.Move(_tokens, positions);
			}

			return hasContender;
		}
	}
}