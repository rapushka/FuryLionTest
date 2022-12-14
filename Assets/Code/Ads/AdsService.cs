using UnityEngine;
using UnityEngine.Advertisements;

namespace Code.Ads
{
	public class AdsService : MonoBehaviour, IUnityAdsInitializationListener
	{
		[SerializeField] private string _androidGameId = "4965819";
		[SerializeField] private string _iOSGameId = "4965818";
		[SerializeField] private bool _testMode = true;
		[SerializeField] private AdsOnLose _adsOnLose;

		private string _gameId;

		private void Awake() => InitializeAds();

		public void OnInitializationComplete() => _adsOnLose.LoadAd();

		public void OnInitializationFailed(UnityAdsInitializationError error, string message)
			=> Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
		
		public void ShowAd() => _adsOnLose.ShowAd();

		private void InitializeAds()
		{
			SetIdByPlatform();

			Advertisement.Initialize(_gameId, _testMode, this);
		}

		private void SetIdByPlatform()
			=> _gameId = Application.platform == RuntimePlatform.IPhonePlayer
				? _iOSGameId
				: _androidGameId;
	}
}