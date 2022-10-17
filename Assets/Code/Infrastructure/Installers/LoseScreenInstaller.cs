using Code.Ads;
using Code.Extensions.DiContainerExtensions;
using Code.Infrastructure.ScenesTransfers;
using Code.Infrastructure.Signals.GameLoop;
using Code.UI;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
	public class LoseScreenInstaller : MonoInstaller
	{
		[SerializeField] private ResetButton _resetButton;
		[SerializeField] private AdsInitializer _adsInitializer;

		public override void InstallBindings()
		{
			Container
				.BindSingleFromInstance(_resetButton)
				.BindSingleFromInstance(_adsInitializer)
				;

			Container.BindSignalTo<ResetButtonClickSignal, SceneTransfer>((x) => x.ToGameplayScene);
		}
	}
}