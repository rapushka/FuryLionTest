using Code.Ads;
using Code.Extensions.DiContainerExtensions;
using Code.Infrastructure.ScenesTransfers;
using Code.Infrastructure.Signals.GameLoop;
using Code.UI.Buttons;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
	public class LoseScreenInstaller : MonoInstaller
	{
		[SerializeField] private ResetButton _resetButton;
		[SerializeField] private AdsService _adsService;

		public override void InstallBindings()
		{
			Container
				.BindSingleFromInstance(_resetButton)
				.BindSingleFromInstance(_adsService)
				;

			Container.BindSignalTo<ResetButtonClickSignal, SceneTransfer>((x) => x.ToGameplayScene);
		}
	}
}