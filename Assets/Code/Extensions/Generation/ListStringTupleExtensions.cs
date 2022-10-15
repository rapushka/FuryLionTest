using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code.Extensions.Generation
{
	public static class ListStringTupleExtensions
	{
		public static string GetMethodParameters(this List<(string type, string name)> @this)
			=> FormatParameters(@this, prefix: string.Empty, (p) => p.ForMethod());

		public static string GetInvokeParameters(this List<(string type, string name)> @this)
			=> FormatParameters(@this, prefix: ", ", (p) => p.ForInvoke());

		public static string GetBaseImmutableSignal(this List<(string type, string name)> @this)
			=> @this.Any()
				? $": ImmutableSignal<{@this.FormatParameters(string.Empty, (p) => $"{p.type}, ")}>"
				: string.Empty;

		private static string FormatParameters
			(this List<(string, string)> @this, string prefix, Func<(string type, string name), string> getEntry)
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