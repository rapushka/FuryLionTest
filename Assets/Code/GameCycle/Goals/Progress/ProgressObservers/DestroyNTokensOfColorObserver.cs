using Code.GameCycle.Goals.Conditions;
using Code.Gameplay.Tokens;

namespace Code.GameCycle.Goals.Progress.ProgressObservers
{
	public class DestroyNTokensOfColorObserver : ProgressObserver
	{
		private readonly TokenUnit _targetColor;
		
		private int _destroyTokensRemain;

		public DestroyNTokensOfColorObserver(DestroyNTokensOfColor destroyNTokensOfColor)
		{
			_targetColor = destroyNTokensOfColor.Color;
			_destroyTokensRemain = destroyNTokensOfColor.TargetCount;
		}

		public void OnTokenDestroyed(TokenUnit tokenUnit)
		{
			if (tokenUnit != _targetColor)
			{
				return;
			}
			
			_destroyTokensRemain--;

			if (_destroyTokensRemain <= 0)
			{
				GoalReachedInvoke();
			} 
		}
	}
}