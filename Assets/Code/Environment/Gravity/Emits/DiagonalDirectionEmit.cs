namespace Code.Environment.Gravity.Emits
{
	public class DiagonalDirectionEmit : BaseDirectionEmit
	{
		public DiagonalDirectionEmit()
			: base(new DiagonallyChecker(), new DiagonallyMover()) { }
	}
}