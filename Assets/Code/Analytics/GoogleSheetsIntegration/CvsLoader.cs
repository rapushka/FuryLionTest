using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Code.Analytics.GoogleSheetsIntegration
{
	public class CvsLoader : MonoBehaviour
	{
		private const string URL = "https://docs.google.com/spreadsheets/d/*/export?format=csv";

		public void DownloadTable(string sheetId, Action<string> onSheetLoadedAction)
		{
			// sheetId = 1A9Zk0BHFY8-hhSt-A_IZs2s7Z9pjylu4GNhd65EcFMk
			var actualUrl = URL.Replace("*", sheetId);
			StartCoroutine(DownloadRawCvsTable(actualUrl, onSheetLoadedAction));
		}

		private IEnumerator DownloadRawCvsTable(string actualUrl, Action<string> callback)
		{
			using var request = UnityWebRequest.Get(actualUrl);
			yield return request.SendWebRequest();

			if (request.result is UnityWebRequest.Result.ConnectionError
			    or UnityWebRequest.Result.ProtocolError
			    or UnityWebRequest.Result.DataProcessingError)
			{
				Debug.LogError(request.error);
			}
			else
			{
#if DEBUG
				Debug.Log("Successful download");
				Debug.Log(request.downloadHandler.text);
#endif

				callback.Invoke(request.downloadHandler.text);
			}

			yield return null;
		}
	}
}