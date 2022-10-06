using Code.Gameplay.Tokens;

namespace Code.GameLoop.Goals.Progress.ProgressObservers
{
	public abstract class DestroyTokensOfTypeObserver : ProgressObserver
	{
		private readonly TokenUnit _targetUnit;

		private int _countRemain;

		protected DestroyTokensOfTypeObserver(int countRemain, TokenUnit targetUnit)
		{
			_countRemain = countRemain;
			_targetUnit = targetUnit;
		}

		public void OnTokenDestroyed(TokenUnit unit)
		{
			if (unit != _targetUnit)
			{
				return;
			}

			_countRemain--;

			if (_countRemain <= 0)
			{
				GoalReachedInvoke();
			}
		}
	}
}