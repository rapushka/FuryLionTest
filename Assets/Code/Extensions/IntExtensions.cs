namespace Code.Extensions
{
	public static class IntExtensions
	{
		public static bool IsInLimited(this int @this, int min, int max) => @this > min && @this < max;
	}
}