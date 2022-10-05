using Code.Environment;
using Code.GameCycle.Goals.Conditions;

namespace Code.GameCycle.Goals.Progress.ProgressObservers
{
	public class DestroyAllObstaclesOfTypeObserver : DestroyTokensOfTypeObserver
	{
		public DestroyAllObstaclesOfTypeObserver(DestroyAllObstaclesOfType goal, Field field)
			: base(field.Count((t) => t.TokenUnit == goal.Type), goal.Type) { }
	}
}