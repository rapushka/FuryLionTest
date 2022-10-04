using Code.GameCycle.Goals.Conditions;
using Code.GameCycle.Goals.TokensTypes;

namespace Code.GameCycle.Goals.Progress.ProgressObservers
{
	public class DestroyNTokensOfColorObserver : DestroyTokensOfTypeObserver<TokenColor>
	{
		public DestroyNTokensOfColorObserver(DestroyNTokensOfColor goal)
			: base(goal.TargetCount, goal.Color) { }
	}
}