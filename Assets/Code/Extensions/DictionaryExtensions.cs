using System;
using System.Collections.Generic;

namespace Code.Extensions
{
	public static class DictionaryExtensions
	{
		public static void ForEach<TKey, TValue>
			(this Dictionary<TKey, TValue> @this, Action<KeyValuePair<TKey, TValue>> action)
		{
			foreach (var item in @this)
			{
				action.Invoke(item);
			}
		}
	}
}