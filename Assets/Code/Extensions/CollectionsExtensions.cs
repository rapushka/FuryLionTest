using System;
using System.Collections.Concurrent;
using Unity.VisualScripting;
using UnityEngine;

namespace Code.Extensions
{
	public static class CollectionsExtensions
	{
		public static void ForEach<T>(this T[] @this, Action<T> action) => Array.ForEach(@this, action);
		
		public static void DoubleFor<T>(this T[,] array, Action<T, int, int> action)
		{
			for (var i = 0; i < array.GetLength(0); i++)
			{
				for (var j = 0; j < array.GetLength(1); j++)
				{
					action(array[i, j], i, j);
				}
			}
		}

		public static T First<T>(this T[,] @this, Func<T, bool> predicate)
		{
			foreach (var item in @this)
			{
				if (predicate.Invoke(item))
				{
					return item;
				}
			}

			throw new ArgumentException("array don't contain needed element");
		}
	}
}