using System;
using UnityEngine;

namespace Code.Extensions
{
	public static class VectorExtensions
	{
		public static Vector2Int ToIntVector(this Vector2 @this) => new((int)@this.x, (int)@this.y);

		public static Vector2 SwapXY(this Vector2 @this)
		{
			(@this.x, @this.y) = (@this.y, @this.x);
			return @this;
		}
		
		public static Vector2 SetY(this Vector2 @this, Func<Vector2, float> getY)
		{
			@this.y = getY.Invoke(@this);
			return @this;
		}
	}
}