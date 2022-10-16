using System;
using Code.Extensions;
using Code.Inner;
using UnityEngine;
using UnityEngine.Networking;

namespace Code.Analytics.GoogleSheetsIntegration.CvsLoader
{
	public class GoogleSheetAsCsvDownloader : ICsvLoader
	{
		private readonly string _sheetId;

		public GoogleSheetAsCsvDownloader(string sheetId) => _sheetId = sheetId;

		public void LoadTable(Action<string> onSheetLoaded)
		{
			var actualUrl = Constants.Analytics.SheetExportAsCsvUrl.Replace("*", _sheetId);
			using var request = UnityWebRequest.Get(actualUrl);

			request.WaitForRequestExecuting()
			       .CheckForErrors(OnRequestError);

			onSheetLoaded.Invoke(request.downloadHandler.text);
		}

		private static void OnRequestError(UnityWebRequest unityWebRequest)
			=> Debug.LogError(unityWebRequest.error);
	}
}