namespace Code.Environment.Gravity.Emits
{
	public class VerticalDirectionEmit : BaseDirectionEmit
	{
		public VerticalDirectionEmit()
			: base(new VerticallyChecker(), new VerticallyMover()) { }
	}
}