using Code.Environment;
using Code.GameCycle.Goals.Conditions;
using Code.Gameplay.Tokens;

namespace Code.GameCycle.Goals.Progress.ProgressObservers
{
	public class DestroyAllObstaclesOfTypeObserver : ProgressObserver
	{
		private readonly TokenUnit _targetObstacleType;

		private int _currentObstaclesCountOnField;

		public DestroyAllObstaclesOfTypeObserver(DestroyAllObstaclesOfType goal, Field field)
		{
			_targetObstacleType = goal.Type;
			_currentObstaclesCountOnField = field.Count((t) => t.TokenUnit == _targetObstacleType);
		}

		public void OnTokenDestroyed(TokenUnit tokenUnit)
		{
			if (tokenUnit != _targetObstacleType)
			{
				return;
			}

			_currentObstaclesCountOnField--;

			if (_currentObstaclesCountOnField <= 0)
			{
				GoalReachedInvoke();
			} 
		}
	}
}