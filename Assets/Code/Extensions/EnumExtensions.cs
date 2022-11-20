using System;

namespace Code.Extensions
{
	public static class EnumExtensions
	{
		public static string GetName(this Enum value) => Enum.GetName(value.GetType(), value);
	}
}