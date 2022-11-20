using System.Collections.Generic;
using System.Linq;
using Code.DataStoring.Localizations.LocalsVariables;
using Code.GameLoop.Goals.Conditions;
using Code.GameLoop.Goals.Progress.ProgressObservers;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace Code.UI.Windows.Panels
{
	public class QuestCompletedWindow : UnityWindow, IGoalVisitor
	{
		[SerializeField] private Button _buttonClose;
		[SerializeField] private TextMeshProUGUI _textMesh;
		[SerializeField] private LocalizedString _obstaclesString;
		[SerializeField] private LocalizedString _tokensString;
		[SerializeField] private LocalizedString _scoreString;
		[SerializeField] private Values _values;

		private readonly Stack<string> _stack = new();

		private void Start() => _buttonClose.onClick.AddListener(UserClose);

		private void OnDestroy() => _buttonClose.onClick.RemoveListener(UserClose);

		private void UserClose()
		{
			if (_stack.Any())
			{
				_stack.Pop();
			}

			ActualizeText();
		}

		public void Initialize(ProgressObserver progressObserver)
		{
			progressObserver.Goal.Accept(this);
			ActualizeText();
		}

		public void Visit(DestroyAllObstaclesOfType goal)
			=> _stack.Push(_obstaclesString.GetLocalizedString(goal.Type));

		public void Visit(DestroyNTokensOfColor goal)
			=> _stack.Push(_tokensString.GetLocalizedString(_values.Set(goal.TargetCount, goal.Color)));

		public void Visit(ReachScoreValue goal) => _stack.Push(_scoreString.GetLocalizedString(goal.TargetScoreValue));
		
		private void ActualizeText() => _textMesh.text = _stack.Any() ? _stack.Peek() : string.Empty;
	}
}