using Code.GameLoop.Goals.Progress.ProgressObservers;
using UnityEngine;

namespace Code.UI.GoalViews
{
	public abstract class GoalView : MonoBehaviour
	{
		[SerializeField] private CanvasGroup _canvasGroup;
		private ProgressObserver _observer;

		protected void Construct(ProgressObserver observer)
		{
			_observer = observer;

			_canvasGroup.alpha = 1f;
			_observer.GoalProgress += OnGoalProgress;
			_observer.GoalReached += OnGoalReached;
		}

		protected abstract void OnGoalProgress(ProgressObserver sender, int newValue);

		private void OnDestroy() => _observer.GoalReached -= OnGoalReached;

		private void OnGoalReached(ProgressObserver sender)
		{
			_canvasGroup.alpha = 0.5f;
			_observer.GoalProgress -= OnGoalProgress;
		}
	}
}