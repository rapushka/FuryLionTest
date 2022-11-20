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

		public void Initialize(ProgressObserver progressObserver) => progressObserver.Goal.Accept(this);

		public void Visit(DestroyAllObstaclesOfType goal)
		{
			_obstaclesLocalizedString.Arguments = new object[] { goal.Type.GetName() };
			_textMesh.text = _obstaclesLocalizedString.GetLocalizedString();
		}

		public void Visit(DestroyNTokensOfColor goal)
		{
			_tokensLocalizedString.Arguments = new object[] { goal.TargetCount, goal.Color.GetName() };
			_textMesh.text = _tokensLocalizedString.GetLocalizedString();
		}

		public void Visit(ReachScoreValue goal)
		{
			_scoreLocalizedString.Arguments = new object[] { goal.TargetScoreValue };
			_textMesh.text = _scoreLocalizedString.GetLocalizedString();
		}
	}
}