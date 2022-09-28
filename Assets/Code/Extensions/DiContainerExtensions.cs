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

		public static DiContainer BindSignalTo<TSignal, TObject>
			(this DiContainer @this, Action<TObject, TSignal> handlerGetter)
		{
			@this.DeclareSignal<TSignal>();
			@this.BindSignal<TSignal>().ToMethod(handlerGetter).FromResolve();

			return @this;
		}

		public static DiContainer BindSingle<TContract>(this DiContainer @this)
		{
			@this.Bind<TContract>().AsSingle();
			return @this;
		}
		
		public static DiContainer BindSingleWithInterfaces<TContract>(this DiContainer @this)
		{
			@this.BindInterfacesAndSelfTo<TContract>().AsSingle();
			return @this;
		}

		public static DiContainer BindSingleFromInstance<TContract>(this DiContainer @this, TContract instance)
		{
			@this.Bind<TContract>().FromInstance(instance).AsSingle();
			return @this;
		}
	}
}