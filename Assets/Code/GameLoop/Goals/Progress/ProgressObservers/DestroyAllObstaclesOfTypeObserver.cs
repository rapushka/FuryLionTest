using Code.Environment;
using Code.GameLoop.Goals.Conditions;

namespace Code.GameLoop.Goals.Progress.ProgressObservers
{
	public class DestroyAllObstaclesOfTypeObserver : DestroyTokensOfTypeObserver
	{
		public DestroyAllObstaclesOfTypeObserver(DestroyAllObstaclesOfType goal, Field field)
			: base(field.Count((t) => t == true && t.TokenUnit == goal.Type), goal.Type) { }
	}
}