using System.Text;

namespace Code.Extensions.GoogleSheetsParsing
{
	public static class StringExtensions
	{
		public static string AsMethodName(this string @this)
		{
			var stringBuilder = new StringBuilder();
			var isAfterSpace = true;

			foreach (var item in @this)
			{
				if (char.IsWhiteSpace(item))
				{
					isAfterSpace = true;
					continue;
				}

				var characterByContext = isAfterSpace ? char.ToUpper(item) : char.ToLower(item);
				stringBuilder.Append(characterByContext);

				isAfterSpace = false;
			}

			return stringBuilder.ToString();
		}
	}
}