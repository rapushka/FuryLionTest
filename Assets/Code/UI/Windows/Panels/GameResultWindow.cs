using System;
using Code.Ads;
using UnityEngine;
using Zenject;

namespace Code.UI.Windows.Panels
{
	public class GameResultWindow : UnityWindow
	{
		[SerializeField] private GameObject _loseView;
		[SerializeField] private GameObject _victoryView;
		private AdsService _adsService;

		[Inject] public void Construct(AdsService adsService) => _adsService = adsService;

		public void Initialize(SessionResult result)
		{
			ChoiceTextMesh(result).SetActive(true);
			ShowAdOnLose(result);
		}

		private GameObject ChoiceTextMesh(SessionResult sessionResult)
			=> sessionResult switch
			{
				SessionResult.Lose    => _loseView,
				SessionResult.Victory => _victoryView,
				var _                 => throw new ArgumentException(nameof(sessionResult)),
			};

		private void ShowAdOnLose(SessionResult result)
		{
			if (result is SessionResult.Lose)
			{
				_adsService.ShowAd();
			}
		}
	}
}