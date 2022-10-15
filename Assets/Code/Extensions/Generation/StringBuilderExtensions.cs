using System.Text;

namespace Code.Extensions.GoogleSheetsParsing
{
	public static class StringBuilderExtensions
	{
		public static void RemoveLasSymbols(this StringBuilder @this, int count)
			=> @this.Remove(@this.Length - count, count);
	}
}