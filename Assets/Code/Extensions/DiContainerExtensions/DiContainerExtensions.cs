namespace Code.Extensions.DiContainerExtensions
{
	public static class DiContainerExtensions
	{
		public static Zenject.DiContainer BindSingle<TContract>(this Zenject.DiContainer @this)
		{
			@this.Bind<TContract>().AsSingle();
			return @this;
		}

		public static Zenject.DiContainer BindInterfaceSingleTo<TContract, TRealization>(this Zenject.DiContainer @this)
			where TRealization : TContract
		{
			@this.Bind<TContract>().To<TRealization>().AsSingle();
			return @this;
		}

		public static Zenject.DiContainer BindSingleWithInterfaces<TContract>(this Zenject.DiContainer @this)
		{
			@this.BindInterfacesAndSelfTo<TContract>().AsSingle();
			return @this;
		}

		public static Zenject.DiContainer BindSingleFromInstance<TContract>(this Zenject.DiContainer @this, TContract instance)
		{
			@this.Bind<TContract>().FromInstance(instance).AsSingle();
			return @this;
		}

		public static Zenject.DiContainer BindSingleFromInstanceWithInterfaces<TContract>
			(this Zenject.DiContainer @this, TContract instance)
		{
			@this.BindInterfacesAndSelfTo<TContract>().FromInstance(instance).AsSingle();
			return @this;
		}
	}
}