using Code.GameLoop.Goals.Conditions;
using Code.Gameplay.TokensField;

namespace Code.GameLoop.Goals.Progress.ProgressObservers
{
	public class DestroyAllObstaclesOfTypeObserver : DestroyTokensOfTypeObserver
	{
		public DestroyAllObstaclesOfTypeObserver(DestroyAllObstaclesOfType goal, Field field)
			: base(goal, field.Count((t) => t == true && t.TokenUnit == goal.Type), goal.Type) { }
	}
}