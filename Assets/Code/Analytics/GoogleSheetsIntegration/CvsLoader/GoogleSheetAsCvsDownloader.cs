using System;
using Code.Extensions;
using UnityEngine;
using UnityEngine.Networking;

namespace Code.Analytics.GoogleSheetsIntegration.CvsLoader
{
	public class GoogleSheetAsCvsDownloader : ICvsLoader
	{
		private const string Url = "https://docs.google.com/spreadsheets/d/*/export?format=csv";

		private readonly string _sheetId;

		public GoogleSheetAsCvsDownloader(string sheetId) => _sheetId = sheetId;

		public void LoadTable(Action<string> onSheetLoaded)
		{
			var actualUrl = Url.Replace("*", _sheetId);
			using var request = UnityWebRequest.Get(actualUrl);

			request.WaitForRequestExecuting()
			       .CheckForErrors(OnRequestError);

			onSheetLoaded.Invoke(request.downloadHandler.text);
		}

		private static void OnRequestError(UnityWebRequest unityWebRequest)
			=> Debug.LogError(unityWebRequest.error);
	}
}