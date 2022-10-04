using System;
using UnityEngine;

namespace Code.Extensions
{
	public static class VectorExtensions
	{
		public static Vector2 VectorAbs(this Vector2 @this) => @this.SetX(Mathf.Abs(@this.x)).SetY(Mathf.Abs(@this.y));

		public static Vector2 SetX(this Vector2 @this, float x)
		{
			@this.x = x;
			return @this;
		}

		public static Vector2 SetY(this Vector2 @this, float y)
		{
			@this.y = y;
			return @this;
		}

		public static Vector2Int ToVectorInt(this Vector2 @this) => new((int)@this.x, (int)@this.y);

		public static Vector2Int ToVectorInt(this Vector3 @this) => new((int)@this.x, (int)@this.y);

		public static Vector2 DistanceTo(this Vector2 @this, Vector2 other) => @this - other;

		public static bool IsLessIncluding(this Vector2 @this, float value) => @this.x <= value && @this.y <= value;

		public static void ForX(this Vector2 @this, float from, float to, Action<Vector2> action)
		{
			for (@this.x = from; @this.x < to; @this.x++)
			{
				action.Invoke(@this);
			}
		}

		public static void DoubleFor(this Vector2 @this, Vector2 from, Vector2 to, Action<Vector2> action)
		{
			for (@this.x = from.x; @this.x < to.x; @this.x++)
			{
				for (@this.y = from.y; @this.y < to.y; @this.y++)
				{
					action.Invoke(@this);
				}
			}
		}

		public static bool IsInBouncesIncluding(this Vector2 @this, Vector2 min, Vector2 max)
			=> @this.x >= min.x
			   && @this.x <= max.x
			   && @this.y >= min.y
			   && @this.y <= max.y;
	}
}