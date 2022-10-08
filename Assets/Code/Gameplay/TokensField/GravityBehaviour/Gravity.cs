using Code.Gameplay.Tokens;
using Code.Gameplay.TokensField.GravityBehaviour.Checkers;
using Code.Gameplay.TokensField.GravityBehaviour.Movers;
using Zenject;

namespace Code.Gameplay.TokensField.GravityBehaviour
{
	public class Gravity
	{
		private readonly DirectionEmit _vertical;
		private readonly DirectionEmit _diagonal;

		private Token[,] _tokens;

		[Inject]
		public Gravity(TokensViewsMover tokensViewsMover)
		{
			_vertical = new DirectionEmit(new VerticallyChecker(), new VerticallyMover(), tokensViewsMover);
			_diagonal = new DirectionEmit(new DiagonallyChecker(), new DiagonallyMover(), tokensViewsMover);
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