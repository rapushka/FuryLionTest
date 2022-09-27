using System;
using UnityEngine;

namespace Code.Extensions
{
	public static class VectorExtensions
	{
		public static Vector2Int AsIndexes(this Vector2 @this, Vector2 offset, float step, float lengthY)
			=> @this
			   .Set((p) => p - offset)
			   .Set((p) => p / step)
			   .SetY(p => p.ReverseY(lengthY))
			   .SwapXY()
			   .ToVectorInt();

		public static Vector2 SwapXY(this Vector2 @this)
		{
			(@this.x, @this.y) = (@this.y, @this.x);
			return @this;
		}

		public static Vector2 SetX(this Vector2 @this, float x)
		{
			@this.x = x;
			return @this;
		}

		public static Vector2 SetY(this Vector2 @this, Func<Vector2, float> getY)
		{
			@this.y = getY.Invoke(@this);
			return @this;
		}

		public static Vector2 SetY(this Vector2 @this, float y)
		{
			@this.y = y;
			return @this;
		}

		public static float ReverseY(this Vector2 @this, float lengthY) => Mathf.Abs(@this.y - (lengthY - 1));

		public static Vector2Int ToVectorInt(this Vector2 @this) => new((int)@this.x, (int)@this.y);
		public static Vector2Int ToVectorInt(this Vector3 @this) => new((int)@this.x, (int)@this.y);
		
		public static Vector2 DistanceTo(this Vector2 @this, Vector2 other) => @this - other;

		public static Vector2 AsAbs(this Vector2 @this) => @this.SetX(Mathf.Abs(@this.x)).SetY(Mathf.Abs(@this.y));

		public static bool LessThanOrEqualTo(this Vector2 @this, float value) => @this.x <= value && @this.y <= value;

		public static Vector3 AsVector3(this Vector2Int @this) => new(@this.x, @this.y);
	}
}