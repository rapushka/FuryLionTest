using System;

namespace Code.Workflow.Extensions
{
	public static class GenericExtensions
	{
		public static T Do<T>(this T @this, Action<T> action, bool @if)
		{
			if (@if)
			{
				action.Invoke(@this);
			}

			return @this;
		}
	}
}