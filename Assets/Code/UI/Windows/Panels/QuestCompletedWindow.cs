using Code.DataStoring.Localizations.LocalsVariables;
using Code.GameLoop.Goals.Conditions;
using Code.GameLoop.Goals.Progress.ProgressObservers;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

namespace Code.UI.Windows.Panels
{
	public class QuestCompletedWindow : UnityWindow, IGoalVisitor
	{
		[SerializeField] private TextMeshProUGUI _textMesh;
		[SerializeField] private LocalizedString _obstaclesString;
		[SerializeField] private LocalizedString _tokensString;
		[SerializeField] private LocalizedString _scoreString;
		[SerializeField] private Values _values;

		public void Initialize(ProgressObserver progressObserver)
		{
			progressObserver.Goal.Accept(this);
		}

		public void Visit(DestroyAllObstaclesOfType goal)
			=> _textMesh.text = _obstaclesString.GetLocalizedString(goal.Type);

		public void Visit(DestroyNTokensOfColor goal)
			=> _textMesh.text = _tokensString.GetLocalizedString(_values.Set(goal.TargetCount, goal.Color));

		public void Visit
			(ReachScoreValue goal)
			=> _textMesh.text = _scoreString.GetLocalizedString(goal.TargetScoreValue);
	}
}