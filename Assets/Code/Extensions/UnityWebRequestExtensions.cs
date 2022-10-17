using System;
using UnityEngine.Networking;

namespace Code.Extensions
{
	public static class UnityWebRequestExtensions
	{
		public static UnityWebRequest WaitForRequestExecuting(this UnityWebRequest @this)
		{
			@this.SendWebRequest();
			while (@this.isDone == false) { }

			return @this;
		}

		public static void CheckForErrors(this UnityWebRequest @this, Action<UnityWebRequest> onError)
		{
			if (@this.result is UnityWebRequest.Result.ConnectionError
			    or UnityWebRequest.Result.ProtocolError
			    or UnityWebRequest.Result.DataProcessingError)
			{
				onError.Invoke(@this);
			}
		}
	}
}