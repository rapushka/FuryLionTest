using System;
using System.Collections.Generic;

namespace Code.Extensions
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

		public static T Do<T>(this T @this, Action<T> action, Func<T, bool> @if)
			=> @this.Do(action, @if.Invoke(@this));

		public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>
			(this TKey @this, Func<TKey, TKey> getKey, Func<TKey, TValue> getValue)
		{
			return new Dictionary<TKey, TValue>
			{
				[getKey(@this)] = getValue(@this)
			};
		}

		public static T Do<T>(this T @this, Action action, bool @if)
		{
			if (@if)
			{
				action.Invoke();
			}

			return @this;
		}
	}
}