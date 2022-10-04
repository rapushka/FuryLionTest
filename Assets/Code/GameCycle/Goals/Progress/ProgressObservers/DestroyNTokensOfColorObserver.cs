using Code.GameCycle.Goals.Conditions;

namespace Code.GameCycle.Goals.Progress.ProgressObservers
{
	public class DestroyNTokensOfColorObserver : DestroyTokensOfTypeObserver
	{
		public DestroyNTokensOfColorObserver(DestroyNTokensOfColor goal)
			: base(goal.TargetCount, goal.Color) { }
	}
}