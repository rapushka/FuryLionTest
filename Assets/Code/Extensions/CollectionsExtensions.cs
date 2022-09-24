using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Extensions
{
	public static class CollectionsExtensions
	{
		public static void ForEach<T>(this T[] @this, Action<T> action) => Array.ForEach(@this, action);

		public static void DoubleFor<T>(this T[,] array, Action<T, int, int> @this)
		{
			for (var i = 0; i < array.GetLength(0); i++)
			{
				for (var j = 0; j < array.GetLength(1); j++)
				{
					@this.Invoke(array[i, j], i, j);
				}
			}
		}

		public static void DoubleForReversed<T>(this T[,] @this, Action<T, int, int> action)
		{
			for (var i = @this.GetLength(0) - 1; i >= 0; i--)
			{
				for (var j = @this.GetLength(1) - 1; j >= 0; j--)
				{
					action.Invoke(@this[i, j], i, j);
				}
			}
		}

		[CanBeNull]
		public static T FirstOrDefaultFromEnd<T>(this T[,] @this, Func<T, int, int, bool> predicate)
		{
			for (var i = @this.GetLength(0) - 1; i >= 0; i--)
			{
				for (var j = @this.GetLength(1) - 1; j >= 0; j--)
				{
					if (predicate.Invoke(@this[i, j], i, j))
					{
						return @this[i, j];
					}
				}
			}

			return default;
		}

		public static T GetAtVector<T>(this T[,] @this, Vector2Int position) => @this[position.x, position.y];

		public static T SetAtVector<T>(this T[,] @this, Vector2Int position, T value)
			=> @this[position.x, position.y] = value;
	}
}