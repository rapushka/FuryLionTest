using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Code.Ads
{
	public class AdsOnLose : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
	{
		[SerializeField] private string _androidAdUnitId = "Interstitial_Android";
		[SerializeField] private string _iOsAdUnitId = "Interstitial_iOS";

		private string _adUnitId;

		private void Awake()
			=> _adUnitId = Application.platform == RuntimePlatform.IPhonePlayer
				? _iOsAdUnitId
				: _androidAdUnitId;
		
		public void LoadAd() => Advertisement.Load(_adUnitId, this);

		public void ShowAd() => Advertisement.Show(_adUnitId, this);

		public void OnUnityAdsAdLoaded(string adUnitId) { }

		public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
		{
			Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
		}

		public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
		{
			Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
		}

		public void OnUnityAdsShowStart(string adUnitId) { }

		public void OnUnityAdsShowClick(string adUnitId) { }

		public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState) { }
	}
}