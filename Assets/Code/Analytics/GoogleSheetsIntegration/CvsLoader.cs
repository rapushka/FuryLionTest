using System;
using UnityEngine.Networking;

namespace Code.Analytics.GoogleSheetsIntegration
{
	public class CvsLoader
	{
		private const string URL = "https://docs.google.com/spreadsheets/d/*/export?format=csv";

		public async void DownloadTable(string sheetId, Action<string> onSheetLoadedAction)
		{
			var actualUrl = URL.Replace("*", sheetId);
			using var request = UnityWebRequest.Get(actualUrl);

			request.SendWebRequest();
			while (request.isDone == false) { }

			HandleErrors(request);
			onSheetLoadedAction.Invoke(request.downloadHandler.text);
		}

		private static void HandleErrors(UnityWebRequest request)
		{
			if (request.result is UnityWebRequest.Result.ConnectionError
			    or UnityWebRequest.Result.ProtocolError
			    or UnityWebRequest.Result.DataProcessingError)
			{
				throw new InvalidOperationException(request.error);
			}
		}
	}
}