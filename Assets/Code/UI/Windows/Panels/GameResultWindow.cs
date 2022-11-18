using System;
using Code.Ads;
using UnityEngine;

namespace Code.UI.Windows.Panels
{
	public class GameResultWindow : UnityWindow
	{
		[SerializeField] private GameObject _loseView;
		[SerializeField] private GameObject _victoryView;

		public void Construct(SessionResult result, AdsService adsService)
		{
			ChoiceTextMesh(result).SetActive(true);
			ShowAdOnLose(result, adsService);
		}

		private GameObject ChoiceTextMesh(SessionResult sessionResult)
			=> sessionResult switch
			{
				SessionResult.Lose    => _loseView,
				SessionResult.Victory => _victoryView,
				_                     => throw new ArgumentException(nameof(sessionResult)),
			};

		private void ShowAdOnLose(SessionResult result, AdsService adsService)
		{
			if (result is SessionResult.Lose)
			{
				adsService.ShowAd();
			}
		}
	}
}