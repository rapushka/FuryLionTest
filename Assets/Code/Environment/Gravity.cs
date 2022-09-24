using Code.Gameplay;

namespace Code.Environment
{
	public class Gravity
	{
		private readonly VerticallyChecker _verticallyChecker;
		private readonly VerticallyMover _verticallyMover;
		private readonly DiagonallyChecker _diagonallyChecker;
		private readonly DiagonallyMover _diagonallyMover;

		private Token[,] _tokens;
		private bool _mayBePrecedents;

		public Gravity()
		{
			_verticallyChecker = new VerticallyChecker();
			_verticallyMover = new VerticallyMover();
			_diagonallyChecker = new DiagonallyChecker();
			_diagonallyMover = new DiagonallyMover();
		}

		public Token[,] Apply(Token[,] tokens)
		{
			_mayBePrecedents = true;

			while (_mayBePrecedents)
			{
				tokens = VerticallyCheck(tokens);
				tokens = DiagonallyCheck(tokens);
			}

			return tokens;
		}

		private Token[,] VerticallyCheck(Token[,] tokens)
		{
			if (_verticallyChecker.HasPrecedentTokens(tokens, out var positions) == false)
			{
				_mayBePrecedents = false;
				return tokens;
			}

			tokens = _verticallyMover.Move(tokens, positions);
			return tokens;
		}

		private Token[,] DiagonallyCheck(Token[,] tokens)
		{
			if (_mayBePrecedents)
			{
				return tokens;
			}
			
			if (_diagonallyChecker.HasPrecedentToken(tokens, out var position, out var direction) == false
			    || position is null)
			{
				return tokens;
			}

			tokens = _diagonallyMover.Move(tokens, position.Value, direction);
			_mayBePrecedents = true;
			return tokens;
		}
	}
}