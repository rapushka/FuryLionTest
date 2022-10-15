using System;
using Code.Extensions;
using UnityEngine;
using UnityEngine.Networking;

namespace Code.Analytics.GoogleSheetsIntegration
{
	public class CvsLoader
	{
		private readonly string _sheetId;
		private const string Url = "https://docs.google.com/spreadsheets/d/*/export?format=csv";

		public CvsLoader(string sheetId) => _sheetId = sheetId;

		public void DownloadTable(Action<string> onSheetLoadedAction)
		{
			var actualUrl = Url.Replace("*", _sheetId);
			using var request = UnityWebRequest.Get(actualUrl);

			request.WaitForRequestExecuting()
			       .CheckForErrors(OnRequestError);

			onSheetLoadedAction.Invoke(request.downloadHandler.text);
		}

		private static void OnRequestError(UnityWebRequest unityWebRequest)
			=> Debug.LogError(unityWebRequest.error);
	}
}