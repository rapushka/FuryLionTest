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

		public Gravity()
		{
			_verticallyChecker = new VerticallyChecker();
			_verticallyMover = new VerticallyMover();
			_diagonallyChecker = new DiagonallyChecker();
			_diagonallyMover = new DiagonallyMover();
		}

		public Token[,] Apply(Token[,] tokens)
		{
			while (true)
			{
				if (_verticallyChecker.HasTokensToMoveVertically(tokens, out var verticallyIndexes))
				{
					tokens = _verticallyMover.Move(tokens, verticallyIndexes);
					continue;
				}

				if (_diagonallyChecker.HasTokenToMoveDiagonally(tokens, out var diagonallyIndex, out var direction)
				    && diagonallyIndex is not null)
				{
					tokens = _diagonallyMover.Move(tokens, diagonallyIndex, direction);
					continue;
				}
				
				break;
			}

			return tokens;
		}
	}
}