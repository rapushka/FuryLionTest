using System.Collections.Generic;
using UnityEngine;

namespace Code.Analytics.ThirdPartyCode
{
	public class Analytics2
	{
		public void Event(string eName, List<(string, object)> param)
		{
			var str = $"[Analytics 2] [{eName}] ";
        
			foreach (var p in param)
				str += $"({p.Item1} - {p.Item2}) ";
        
			Debug.Log(str);
		}
	}
}