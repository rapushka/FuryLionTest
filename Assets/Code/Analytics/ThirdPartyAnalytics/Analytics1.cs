// ReSharper disable All имитируем код на третьей стороне, потому менять ничего не можем
using UnityEngine;

namespace Code.Analytics.ThirdPartyAnalytics
{
	public class Analytics1
	{
		public void sendEvent(string eName, params string[] param)
		{
			var str = $"[Analytics 1] [{eName}] ";
        
			foreach (var p in param)
				str += $"{p} ";
        
			Debug.Log(str);
		}
	}
}