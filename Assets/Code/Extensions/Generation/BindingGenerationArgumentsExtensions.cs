using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code.Extensions.Generation
{
	public static class BindingGenerationArgumentsExtensions
	{
		public static string GetArgsDeclaration(this IEnumerable<(string type, string name)> parameters)
			=> parameters.Any() ? ", v" : string.Empty;

		public static string GetArgsUsage(this ICollection parameters)
			=> parameters.Count switch
			{
				0 => string.Empty,
				1 => "(v.Value)",
				_ => GenerateArgsForTwoAndMore(parameters.Count)
			};

		private static string GenerateArgsForTwoAndMore(int parametersCount)
		{
			const int postfixLenght = 2;

			var result = new StringBuilder();
			result.Append('(');

			for (var i = 0; i < parametersCount; i++)
			{
				result.Append($"v.Value{i + 1}, ");
			}

			result.RemoveLasSymbols(postfixLenght);
			result.Append(')');

			return result.ToString();
		}
	}
}