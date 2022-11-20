using System;
using Code.Extensions;
using Code.GameLoop.Goals.Conditions;
using Code.GameLoop.Goals.Progress.ProgressObservers;
using TMPro;
using UnityEditor;
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

		public void Initialize(ProgressObserver progressObserver) => progressObserver.Goal.Accept(this);

		public void Visit(DestroyAllObstaclesOfType goal)
			=> _textMesh.text = _obstaclesLocalizedString.GetLocalizedString(goal.Type.GetName());

		public void Visit(DestroyNTokensOfColor goal)
			=> _textMesh.text = _tokensLocalizedString.GetLocalizedString(goal.TargetCount, goal.Color.GetName());

		public void Visit(ReachScoreValue goal)
			=> _textMesh.text = _scoreLocalizedString.GetLocalizedString(goal.TargetScoreValue);
	}
}