using Code.DataStoring.Localizations.LocalsVariables;
using Code.Extensions;
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
		[SerializeField] private LocalizedString _obstaclesLocalizedString;
		[SerializeField] private LocalizedString _tokensLocalizedString;
		[SerializeField] private LocalizedString _scoreLocalizedString;
		[SerializeField] private Values _values;

		public void Initialize(ProgressObserver progressObserver) => progressObserver.Goal.Accept(this);

		public void Visit(DestroyAllObstaclesOfType goal)
			=> _textMesh.text = _obstaclesLocalizedString.GetLocalizedString(goal.Type.GetName());

		public void Visit(DestroyNTokensOfColor goal)
			=> _textMesh.text = _tokensLocalizedString.GetLocalizedString(_values.Set(goal.TargetCount, goal.Color));

		public void Visit(ReachScoreValue goal)
			=> _textMesh.text = _scoreLocalizedString.GetLocalizedString(goal.TargetScoreValue);
	}
}