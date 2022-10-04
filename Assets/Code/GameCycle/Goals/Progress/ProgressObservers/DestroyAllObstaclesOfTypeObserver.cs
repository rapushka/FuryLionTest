using Code.Environment;
using Code.GameCycle.Goals.Conditions;
using Code.GameCycle.Goals.TokensTypes;

namespace Code.GameCycle.Goals.Progress.ProgressObservers
{
	public class DestroyAllObstaclesOfTypeObserver : DestroyTokensOfTypeObserver<ObstacleType>
	{
		public DestroyAllObstaclesOfTypeObserver(DestroyAllObstaclesOfType goal, Field field)
			: base(field.Count((t) => t.TokenUnit == goal.Type), goal.Type) { }
	}
}