using Code.Gameplay.Tokens;
using DG.Tweening;
using Zenject;

namespace Code.View.Animations
{
	public class BonusAnimator
	{
		[Inject] public BonusAnimator() { }

		public void OnBonusSpawned(Token token)
		{
			// ReSharper disable once ConditionIsAlwaysTrueOrFalse - it's not
			if (token.transform != null)
			{
				token.transform.DOShakeScale(0.5f);
			}
		}
	}
}