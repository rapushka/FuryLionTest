using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Extensions
{
	public static class DoubleArrayExtensions
	{
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

		[CanBeNull]
		public static T FirstOrDefault<T>(this T[,] @this, Func<T, int, int, bool> predicate)
		{
			for (var i = 0; i < @this.GetLength(0); i++)
			{
				for (var j = 0; j < @this.GetLength(1); j++)
				{
					if (predicate.Invoke(@this[i, j], i, j))
					{
						return @this[i, j];
					}
				}
			}

			return default;
		}

		public static IEnumerable<T> Where<T>(this T[,] @this, Func<T, bool> predicate)
			=> @this.Where((t, _, _) => predicate(t));

		public static IEnumerable<T> Where<T>(this T[,] @this, Func<T, int, int, bool> predicate)
		{
			for (var i = 0; i < @this.GetLength(0); i++)
			{
				for (var j = 0; j < @this.GetLength(1); j++)
				{
					if (predicate.Invoke(@this[i, j], i, j))
					{
						yield return @this[i, j];
					}
				}
			}
		}

		public static T GetAtVector<T>(this T[,] @this, Vector2Int position)
			=> @this[position.x, position.y];

		public static T SetAtVector<T>(this T[,] @this, Vector2Int position, T value)
			=> @this[position.x, position.y] = value;

		public static Vector2Int IndexesOf<T>(this T[,] @this, T element)
		{
			for (var x = 0; x < @this.GetLength(0); x++)
			{
				for (var y = 0; y < @this.GetLength(1); y++)
				{
					if (Equals(@this[x, y], element))
					{
						return new Vector2Int(x, y);
					}
				}
			}

			throw new ArgumentException("Array don't contain that element");
		}
		public static bool Contain<T>(this T[,] @this, T element)
		{
			for (var x = 0; x < @this.GetLength(0); x++)
			{
				for (var y = 0; y < @this.GetLength(1); y++)
				{
					if (Equals(@this[x, y], element))
					{
						return true;
					}
				}
			}

			return false;
		}
	}
}