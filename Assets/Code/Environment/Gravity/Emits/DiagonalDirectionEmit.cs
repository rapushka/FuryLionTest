using Code.Environment.Gravity.Checkers;
using Code.Environment.Gravity.Movers;

namespace Code.Environment.Gravity.Emits
{
	public class DiagonalDirectionEmit : BaseDirectionEmit
	{
		public DiagonalDirectionEmit()
			: base(new DiagonallyChecker(), new DiagonallyMover()) { }
	}
}