using Code.Ads;
using Code.Extensions.DiContainerExtensions;
using Code.Generated.Analytics.Signals;
using Code.Infrastructure.Signals.Ads;
using Code.Infrastructure.Signals.GameLoop;
using Code.UI.Buttons;
using Code.UI.GameSettings;
using Code.UI.Windows.Service;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
	public class WindowsInstaller : MonoInstaller
	{
		[SerializeField] private WindowsChain _windowsChain;
		[SerializeField] private RestartButton _restartButton;
		[SerializeField] private SettingsWindow _settingsWindow;
		[SerializeField] private LanguageSelector _languageSelector;
		[SerializeField] private SoundSettings _soundSettings;
		[SerializeField] private AdsInitializer _adsInitializer;

		// ReSharper disable Unity.PerformanceAnalysis - метод вызывается только на инициализации
		public override void InstallBindings()
		{
			Container
				.BindSingle<WindowsService>()
				.BindSingleFromInstance(_windowsChain)
				.BindSingleFromInstance(_restartButton)
				.BindSingleFromInstance(_settingsWindow)
				.BindSingleWithInterfaces<Settings>()
				.BindSingleFromInstance(_languageSelector)
				.BindSingleFromInstance(_soundSettings)
				.BindSingleFromInstance(_adsInitializer)
				;

			Container
				.BindSignalTo<GameVictorySignal, WindowsService>((x) => x.OnVictory)
				.BindSignalTo<GameLoseSignal, WindowsService>((x) => x.OnLose)
				.BindSignalTo<SettingsOpenedSignal, WindowsService>((x) => x.OpenSettings)
				.BindSignalTo<ShowAdSignal, AdsInitializer>((x) => x.ShowAd)
				;
		}
	}
}