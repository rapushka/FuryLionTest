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
			if (_verticallyChecker.HasPrecedentTokens(_tokens, out var positions) == false)
			{
				_mayBePrecedents = false;
				return;
			}

			_tokens = _verticallyMover.Move(_tokens, positions);
		}

		private void DiagonallyCheck()
		{
			if (_mayBePrecedents)
			{
				return;
			}
			
			if (_diagonallyChecker.HasPrecedentToken(_tokens, out var position, out var direction) == false
			    || position is null)
			{
				return;
			}

			_tokens = _diagonallyMover.Move(_tokens, position.Value, direction);
			_mayBePrecedents = true;
		}
	}
}