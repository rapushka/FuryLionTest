using Code.Environment.Gravity.Checkers;
using Code.Environment.Gravity.Movers;

namespace Code.Environment.Gravity.Emits
{
	public class VerticalDirectionEmit : BaseDirectionEmit
	{
		public VerticalDirectionEmit()
			: base(new VerticallyChecker(), new VerticallyMover()) { }
	}
}