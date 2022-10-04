using System;
using Code.Extensions;
using Code.Gameplay.Tokens;

namespace Code.GameCycle.Goals.Progress.ProgressObservers
{
	public abstract class DestroyTokensOfTypeObserver<T> : ProgressObserver
	{
		private readonly TokenUnit _targetUnit;

		private int _countRemain;

		protected DestroyTokensOfTypeObserver(int countRemain, TokenUnit targetUnit)
		{
			if (targetUnit.IsDefinedInEnum<T>() == false)
			{
				throw new ArgumentException($"Use this observer only for {typeof(T).Name} Progress");
			}

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