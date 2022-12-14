using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ModestTree;

namespace Code.Extensions.GoogleSheetsParsing
{
	public static class StringCollectionsExtensions
	{
		private static readonly Regex Regex = new(@"\((.+?)\) (\w+)");

		public static List<(string type, string name)> GetParsedParameters(this string[] @this)
			=> @this.GetRawParameters()
			        .LinqQuery()
			        .ToList();

		private static IEnumerable<string> GetRawParameters(this IReadOnlyList<string> @this)
		{
			var parametersStartIndex = 1;
			var parametersEndIndex = @this.Count - 1;

			for (var i = parametersStartIndex; i < parametersEndIndex; i++)
			{
				yield return @this[i];
			}
		}

		private static IEnumerable<(string type, string name)> LinqQuery(this IEnumerable<string> @this)
			=> from parameter in @this
			   where parameter.IsEmpty() == false
			   select Regex.Match(parameter)
			   into match
			   let type = match.Groups[1].Value
			   let name = match.Groups[2].Value
			   select (type, name);
	}
}