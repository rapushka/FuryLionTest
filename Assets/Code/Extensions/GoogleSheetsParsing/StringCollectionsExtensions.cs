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
			=> @this.GetRawParameters().ParseParameters();

		private static IEnumerable<string> GetRawParameters(this string[] @this)
		{
			var parametersStartIndex = 1;
			var parametersEndIndex = @this.Length - 1;
			
			for (var i = parametersStartIndex; i < parametersEndIndex; i++)
			{
				yield return @this[i];
			}
		}

		private static List<(string, string)> ParseParameters(this IEnumerable<string> @this)
			=> LinqQuery(@this)
				.ToList();

		private static IEnumerable<(string type, string name)> LinqQuery(IEnumerable<string> @this)
			=> from parameter in @this
			   where parameter.IsEmpty() == false
			   select Regex.Match(parameter)
			   into match
			   let type = match.Groups[1].Value
			   let name = match.Groups[2].Value
			   select (type, name);
	}
}