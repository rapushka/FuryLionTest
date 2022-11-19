using System;
using Code.GameLoop.Goals.Conditions;

namespace Code.UI.Windows.Panels
{
	public class QuestCompletedWindow : UnityWindow, IGoalVisitor
	{
		public string Visit(DestroyAllObstaclesOfType goal)
		{
			throw new NotImplementedException();
		}

		public string Visit(DestroyNTokensOfColor goal)
		{
			throw new NotImplementedException();
		}

		public string Visit(ReachScoreValue goal)
		{
			throw new NotImplementedException();
		}
	}

	public interface IGoalVisitor
	{
		string Visit(DestroyAllObstaclesOfType goal);
		string Visit(DestroyNTokensOfColor goal);
		string Visit(ReachScoreValue goal);
	}
}