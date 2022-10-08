using System;
using UnityEngine;

namespace Code.Extensions
{
	public static class VectorExtensions
	{
		public static Vector2Int ToVectorInt(this Vector2 @this) => new((int)@this.x, (int)@this.y);

		public static void ForX(this Vector2Int @this, int from, int to, Action<Vector2Int> action)
		{
			for (@this.x = from; @this.x < to; @this.x++)
			{
				action.Invoke(@this);
			}
		}

		public static void DoubleFor(this Vector2Int @this, Vector2Int from, Vector2Int to, Action<Vector2Int> action)
		{
			for (@this.x = from.x; @this.x < to.x; @this.x++)
			{
				for (@this.y = from.y; @this.y < to.y; @this.y++)
				{
					action.Invoke(@this);
				}
			}
		}

		public static bool IsInBouncesIncluding(this Vector2Int @this, Vector2 min, Vector2 max)
			=> @this.x >= min.x
			   && @this.x <= max.x
			   && @this.y >= min.y
			   && @this.y <= max.y;

		public static Vector3 AsVector3(this Vector2Int @this) => new(@this.x, @this.y);
	}
}