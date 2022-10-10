using UnityEngine;
using UnityEngine.Advertisements;

namespace Code.Ads
{
	public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
	{
		[SerializeField] private string _androidGameId;
		[SerializeField] private string _iOSGameId;
		[SerializeField] private bool _testMode = true;
		[SerializeField] private AdsOnLose _adsOnLose;

		private string _gameId;

		private void Awake() => InitializeAds();

		public void InitializeAds()
		{
			SetIdByPlatform();

			Advertisement.Initialize(_gameId, _testMode, this);
		}

		private void SetIdByPlatform()
			=> _gameId = Application.platform == RuntimePlatform.IPhonePlayer
				? _iOSGameId
				: _androidGameId;

		public void OnInitializationComplete() => _adsOnLose.LoadAd();

		public void OnInitializationFailed(UnityAdsInitializationError error, string message)
			=> Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
	}
}