using Code.GameLoop.Goals.Conditions;
using Code.Gameplay.Tokens;

namespace Code.GameLoop.Goals.Progress.ProgressObservers
{
	public abstract class DestroyTokensOfTypeObserver : ProgressObserver
	{
		private int _destroyedCount;

		protected DestroyTokensOfTypeObserver(Goal goal, int targetCount, TokenUnit targetUnit)
			: base(goal)
		{
			TargetCount = targetCount;
			TargetUnit = targetUnit;
		}

		public int TargetCount { get; }

		public TokenUnit TargetUnit { get; }

		public void OnTokenDestroyed(TokenUnit unit)
		{
			if (unit != TargetUnit)
			{
				return;
			}

			_destroyedCount++;
			GoalProgressInvoke(_destroyedCount);

			if (_destroyedCount >= TargetCount)
			{
				GoalReachedInvoke();
			}
		}
	}
}