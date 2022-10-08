using System;
using System.Collections.Generic;
using System.Linq;

namespace Code.Extensions
{
	public static class CollectionsExtensions
	{
		public static void ForEach<T>(this T[] @this, Action<T> action) => Array.ForEach(@this, action);

		public static void ForEach<T>(this IEnumerable<T> @this, Action<T> action)
		{
			foreach (var entry in @this)
			{
				action.Invoke(entry);
			}
		}

		public static T PickRandom<T>(this IEnumerable<T> @this)
		{
			var array = @this as T[] ?? @this.ToArray();

			var randomIndex = UnityEngine.Random.Range(0, array.Length);
			return array[randomIndex];
		}
	}
}