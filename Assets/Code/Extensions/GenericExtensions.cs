using System;

namespace Code.Extensions
{
	public static class GenericExtensions
	{
		public static T Do<T>(this T @this, Action<T> action)
		{
			action.Invoke(@this);
			return @this;
		} 
		
		public static TOut Select<TIn, TOut>(this TIn @this, Func<TIn, TOut> action) => action.Invoke(@this);

		public static T Do<T>(this T @this, Action<T> action, bool @if)
		{
			if (@if)
			{
				action.Invoke(@this);
			}

			return @this;
		}

		public static T Do<T>(this T @this, Func<T, bool> @if, Action<T> @true, Action<T> @false)
		{
			if (@if.Invoke(@this))
			{
				@true.Invoke(@this);
			}
			else
			{
				@false.Invoke(@this);
			}

			return @this;
		}
		
		public static T Do<T>(this T @this, Action<T> action, Func<T, bool> @if) 
			=> @this.Do(action, @if.Invoke(@this));
	}
}