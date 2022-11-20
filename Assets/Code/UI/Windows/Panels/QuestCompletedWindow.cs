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

		private void OnEnable()
		{
			_obstaclesLocalizedString.StringChanged += UpdateText;
			_tokensLocalizedString.StringChanged += UpdateText;
			_scoreLocalizedString.StringChanged += UpdateText;
		}

		private void OnDisable()
		{
			_obstaclesLocalizedString.StringChanged -= UpdateText;
			_tokensLocalizedString.StringChanged -= UpdateText;
			_scoreLocalizedString.StringChanged -= UpdateText;
		}

		public void Initialize(ProgressObserver progressObserver) => progressObserver.Goal.Accept(this);

		public void Visit(DestroyAllObstaclesOfType goal)
			=> _obstaclesLocalizedString.RefreshTextWithArguments(goal.Type.GetName());

		public void Visit(DestroyNTokensOfColor goal)
			=> _tokensLocalizedString.RefreshTextWithArguments(goal.TargetCount, goal.Color.GetName());

		public void Visit(ReachScoreValue goal)
			=> _scoreLocalizedString.RefreshTextWithArguments(goal.TargetScoreValue);

		private void UpdateText(string value) => _textMesh.text = value;
	}

	public static class LocalizedStringExtensions
	{
		public static void RefreshTextWithArguments(this LocalizedString @this, params object[] args)
		{
			@this.Arguments = args;
			@this.RefreshString();
		}
	}
}