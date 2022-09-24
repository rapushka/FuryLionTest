using Code.Gameplay;

namespace Code.Environment
{
	public class Gravity
	{
		private readonly VerticallyChecker _verticallyChecker;
		private readonly VerticallyMover _verticallyMover;

		private Token[,] _tokens;

		public Gravity()
		{
			_verticallyChecker = new VerticallyChecker();
			_verticallyMover = new VerticallyMover();
		}

		public Token[,] Apply(Token[,] tokens)
		{
			while (true)
			{
				if (_verticallyChecker.HasTokensToMoveVertically(tokens, out var indexes))
				{
					tokens = _verticallyMover.Move(tokens, indexes);
					continue;
				}

				break;
			}

			return tokens;
		}
	}
}