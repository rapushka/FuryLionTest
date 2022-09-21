using UnityEngine;

namespace Code.Extensions
{
	public static class RectExtensions
	{
		public static Rect AddY(this Rect @this, float y)
		{
			@this.y += y;
			return @this;
		}

		public static Rect AddX(this Rect @this, float x)
		{
			@this.x += x;
			return @this;
		}

		public static Rect SetX(this Rect @this, float x)
		{
			@this.x = x;
			return @this;
		}

		public static Rect MultipleWidth(this Rect @this, float width)
		{
			@this.width *= width;
			return @this;
		}

		public static Rect SetWidth(this Rect @this, float width)
		{
			@this.width = width;
			return @this;
		}

		public static Rect SetHeight(this Rect @this, float height)
		{
			@this.height = height;
			return @this;
		}
	}
}