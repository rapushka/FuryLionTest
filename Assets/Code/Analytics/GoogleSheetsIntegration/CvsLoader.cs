using System;
using System.Collections;
using Code.Inner.CustomMonoBehaviours;
using UnityEngine.Networking;
using Zenject;

namespace Code.Analytics.GoogleSheetsIntegration
{
	public class CvsLoader
	{
		private readonly CoroutinesHandler _coroutines;
		private const string URL = "https://docs.google.com/spreadsheets/d/*/export?format=csv";

		[Inject] public CvsLoader(CoroutinesHandler coroutines) => _coroutines = coroutines;

		public void DownloadTable(string sheetId, Action<string> onSheetLoadedAction)
		{
			var actualUrl = URL.Replace("*", sheetId);
			_coroutines.StartRoutine(DownloadRawCvsTable(actualUrl, onSheetLoadedAction));
		}

		private IEnumerator DownloadRawCvsTable(string actualUrl, Action<string> callback)
		{
			using var request = UnityWebRequest.Get(actualUrl);
			yield return request.SendWebRequest();

			if (request.result is UnityWebRequest.Result.ConnectionError
			    or UnityWebRequest.Result.ProtocolError
			    or UnityWebRequest.Result.DataProcessingError)
			{
				throw new InvalidOperationException(request.error);
			}

			callback.Invoke(request.downloadHandler.text);
			yield return null;
		}
	}
}