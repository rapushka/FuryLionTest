using System;
using Code.Ads;
using Code.UI.Windows.Service;
using UnityEngine;

namespace Code.UI.Windows.Panels
{
	public class GameResultWindow : UnityWindow
	{
		[SerializeField] private GameObject _loseView;
		[SerializeField] private GameObject _victoryView;
		[SerializeField] private AdsInitializer _ads;

		public void Construct(SessionResult sessionResult)
		{
			ChoiceTextMesh(sessionResult).SetActive(true);
			ShowAdOnLose(sessionResult);
		}

		private void ShowAdOnLose(SessionResult result)
		{
			if (result != SessionResult.Lose)
			{
				return;
			}
			
			_ads.ShowAd();
		}

		private GameObject ChoiceTextMesh(SessionResult sessionResult)
			=> sessionResult switch
			{
				SessionResult.Lose    => _loseView,
				SessionResult.Victory => _victoryView,
				_                     => throw new ArgumentException(nameof(sessionResult)),
			};
	}
}