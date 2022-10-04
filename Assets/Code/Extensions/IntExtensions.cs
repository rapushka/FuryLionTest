namespace Code.Extensions
{
	public static class IntExtensions
	{
		public static bool IsBetweenIncluding(this int @this, int min, int max) => @this >= min && @this <= max;
	}
}