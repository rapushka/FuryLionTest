using Code.GameLoop.Goals.Conditions;
using UnityEngine;

namespace Code.UI.Windows.Panels
{
	public class QuestCompletedWindow : UnityWindow, IGoalVisitor
	{
		[SerializeField] private GameObject _destroyAllObstaclesOfTypeView;
		[SerializeField] private GameObject _destroyNTokensOfColorView;
		[SerializeField] private GameObject _reachScoreValueView;
		
		public void Visit(DestroyAllObstaclesOfType goal) => _destroyAllObstaclesOfTypeView.SetActive(true);

		public void Visit(DestroyNTokensOfColor goal) => _destroyNTokensOfColorView.SetActive(true);

		public void Visit(ReachScoreValue goal) => _reachScoreValueView.SetActive(true);
	}
}