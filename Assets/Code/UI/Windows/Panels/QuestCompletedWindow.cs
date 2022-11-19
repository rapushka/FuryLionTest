using Code.GameLoop.Goals.Conditions;
using TMPro;
using UnityEngine;

namespace Code.UI.Windows.Panels
{
	public class QuestCompletedWindow : UnityWindow, IGoalVisitor
	{
		[SerializeField] private TextMeshProUGUI _destroyAllObstaclesOfTypeView;
		[SerializeField] private TextMeshProUGUI _destroyNTokensOfColorView;
		[SerializeField] private TextMeshProUGUI _reachScoreValueView;
		
		public void Visit(DestroyAllObstaclesOfType goal)
		{
			var tokenUnit = goal.Type;
			_destroyAllObstaclesOfTypeView.gameObject.SetActive(true);
			_destroyAllObstaclesOfTypeView.text = string.Format(_destroyAllObstaclesOfTypeView.text, tokenUnit);
		}

		public void Visit(DestroyNTokensOfColor goal)
		{
			var targetCount = goal.TargetCount;
			var color = goal.Color;
			_destroyNTokensOfColorView.gameObject.SetActive(true);
			_destroyNTokensOfColorView.text = string.Format(_destroyNTokensOfColorView.text, targetCount, color);
		}

		public void Visit(ReachScoreValue goal)
		{
			var targetScore = goal.TargetScoreValue;
			_reachScoreValueView.gameObject.SetActive(true);
			_reachScoreValueView.text = string.Format(_reachScoreValueView.text, targetScore);
		}
	}
}