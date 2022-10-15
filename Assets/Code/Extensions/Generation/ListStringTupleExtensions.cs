using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Code.Extensions.GoogleSheetsParsing;

namespace Code.Extensions.Generation
{
	public static class ListStringTupleExtensions
	{
		public static string GetMethodParameters(this List<(string type, string name)> @this)
			=> FormatParameters(@this, prefix: string.Empty, (p) => p.ForMethod());

		public static string GetInvokeParameters(this List<(string type, string name)> @this)
			=> FormatParameters(@this, prefix: ", ", (p) => p.ForInvoke());

		private static string FormatParameters
			(this List<(string type, string name)> @this, string prefix, Func<(string, string), string> getEntry)
		{
			const int postfixLenght = 2;

			if (@this.Any() == false)
			{
				return string.Empty;
			}

			var result = new StringBuilder();
			result.Append(prefix);
			foreach (var parameter in @this)
			{
				result.Append(getEntry.Invoke(parameter));
			}

			result.RemoveLasSymbols(postfixLenght);

			return result.ToString();
		}
	}
}