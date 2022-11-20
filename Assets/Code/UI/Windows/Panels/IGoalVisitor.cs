using Code.GameLoop.Goals.Conditions;

namespace Code.UI.Windows.Panels
{
	public interface IGoalVisitor
	{
		void Visit(DestroyAllObstaclesOfType goal);
		void Visit(DestroyNTokensOfColor goal);
		void Visit(ReachScoreValue goal);
	}
}