using UnityEngine;

namespace Code.Extensions
{
	public static class VectorExtensions
	{
		public static Vector2Int ToIntVector(this Vector2 @this) => new((int)@this.x, (int)@this.x);
	}
}