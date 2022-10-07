using Code.GameLoop.Goals.Progress.ProgressObservers;
using Code.Gameplay.Tokens;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.GoalViews
{
	public class DestroyTokensGoalView : GoalView
	{
		[SerializeField] private Image _tokenSprite;
		[SerializeField] private TextMeshProUGUI _text;

		private int _targetCount;
		private TokenUnit _unit;

		public void Construct(ProgressObserver observer, int targetCount, Sprite tokenSprite)
		{
			base.Construct(observer);
			
			_targetCount = targetCount;
			_tokenSprite.sprite = tokenSprite;

			UpdateView(0);
		}
		
		protected override void OnGoalProgress(ProgressObserver sender, int newValue) 
			=> UpdateView(newValue);

		private void UpdateView(int newValue) => _text.text = $"{newValue} / {_targetCount}";
	}
}