using Code.Environment;
using Code.GameCycle.Goals.Conditions;
using Code.Gameplay.Tokens;

namespace Code.GameCycle.Goals.Progress.ProgressObservers
{
	public class DestroyAllObstaclesOfTypeObserver : ProgressObserver
	{
		private readonly TokenUnit _targetObstacleType;

		private int _obstaclesDestroyRemain;

		public DestroyAllObstaclesOfTypeObserver(DestroyAllObstaclesOfType goal, Field field)
		{
			_targetObstacleType = goal.Type;
			_obstaclesDestroyRemain = field.Count((t) => t.TokenUnit == _targetObstacleType);
		}

		public void OnTokenDestroyed(TokenUnit tokenUnit)
		{
			if (tokenUnit != _targetObstacleType)
			{
				return;
			}

			_obstaclesDestroyRemain--;

			if (_obstaclesDestroyRemain <= 0)
			{
				GoalReachedInvoke();
			} 
		}
	}
}