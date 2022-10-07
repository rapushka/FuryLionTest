using Code.GameLoop.Goals.Conditions;

namespace Code.GameLoop.Goals.Progress.ProgressObservers
{
	public class DestroyNTokensOfColorObserver : DestroyTokensOfTypeObserver
	{
		public DestroyNTokensOfColorObserver(DestroyNTokensOfColor goal)
			: base(goal.TargetCount, goal.Color) { }
	}
}