using System;
using Zenject;

namespace Code.Extensions
{
	public static class DiContainerExtensions
	{
		public static DiContainer BindSignalTo<TSignal, TObject>
			(this DiContainer @this, Func<TObject, Action> handlerGetter)
		{
			@this.DeclareSignal<TSignal>();

			@this.BindSignal<TSignal>().ToMethod(handlerGetter).FromResolve();
			return @this;
		}
	}
}