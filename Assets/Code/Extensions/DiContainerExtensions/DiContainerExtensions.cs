using UnityEngine;
using Zenject;

namespace Code.Extensions.DiContainerExtensions
{
	public static class DiContainerExtensions
	{
		public static DiContainer BindSingle<TContract>(this DiContainer @this)
		{
			@this.Bind<TContract>().AsSingle();
			return @this;
		}

		public static DiContainer BindInterfaceSingleTo<TContract, TRealization>(this DiContainer @this)
			where TRealization : TContract
		{
			@this.Bind<TContract>().To<TRealization>().AsSingle();
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

		public static DiContainer BindSingleFromInstanceWithInterfaces<TContract>
			(this DiContainer @this, TContract instance)
		{
			@this.BindInterfacesAndSelfTo<TContract>().FromInstance(instance).AsSingle();
			return @this;
		}
		
		public static DiContainer BindSinglePrefabAsDontDestroy<TContract>(this DiContainer @this, TContract prefab)
			where TContract : Object
			=> @this.BindSingleFromInstance(InstantiateDontDestroy(prefab));

		private static TObject InstantiateDontDestroy<TObject>(TObject prefab)
			where TObject : Object
		{
			var instance = Object.Instantiate(prefab);
			Object.DontDestroyOnLoad(instance);
			return instance;
		}

	}
}