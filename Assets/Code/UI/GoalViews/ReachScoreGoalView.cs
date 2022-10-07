using Code.GameLoop.Goals.Progress.ProgressObservers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.GoalViews
{
	public class ReachScoreGoalView : GoalView
	{
		[SerializeField] private Image _mask;
		[SerializeField] private TextMeshProUGUI _text;

		private int _targetValue;

		public void Construct(ProgressObserver observer, int targetValue)
		{
			base.Construct(observer);

			_targetValue = targetValue;
			UpdateView(0);
		}

		protected override void OnGoalProgress(ProgressObserver sender, int newValue) => UpdateView(newValue);

		private void UpdateView(int newValue)
		{
			if (newValue > _targetValue)
			{
				newValue = _targetValue;
			}

			_mask.fillAmount = (float)newValue / _targetValue;
			_text.text = $"{newValue} / {_targetValue}";
		}
	}
}