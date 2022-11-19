using Code.GameLoop.Goals.Conditions;
using Code.GameLoop.Goals.Progress.ProgressObservers;
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
			var text = _destroyAllObstaclesOfTypeView.text;
			
			// replace {0} with variable
			_destroyAllObstaclesOfTypeView.text = string.Format(text, tokenUnit);
		}

		public void Visit(DestroyNTokensOfColor goal)
		{
			var targetCount = goal.TargetCount;
			var color = goal.Color;
			_destroyNTokensOfColorView.gameObject.SetActive(true);
			
			var text = _destroyNTokensOfColorView.text;
			// replace {0} and {1} with variables
			_destroyNTokensOfColorView.text = string.Format(text, targetCount, color);
		}

		public void Visit(ReachScoreValue goal)
		{
			var targetScore = goal.TargetScoreValue;
			_reachScoreValueView.gameObject.SetActive(true);
			
			var text = _reachScoreValueView.text;
			// replace {0} with variable
			_reachScoreValueView.text = string.Format(text, targetScore);
		}

		private void OnDisable()
		{
			_destroyAllObstaclesOfTypeView.gameObject.SetActive(false);
			_destroyNTokensOfColorView.gameObject.SetActive(false);
			_reachScoreValueView.gameObject.SetActive(false);
		}

		public void Initialize(ProgressObserver progressObserver) => progressObserver.Goal.Accept(this);
	}
}