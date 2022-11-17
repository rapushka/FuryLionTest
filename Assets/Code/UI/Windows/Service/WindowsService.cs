using Code.DataStoring;
using Code.UI.GameSettings;
using Code.UI.Windows.Panels;
using Zenject;

namespace Code.UI.Windows.Service
{
	public class WindowsService
	{
		private readonly WindowsChain _windowsChain;

		private readonly IStorage _storage;
		private readonly LanguageSelector _languageSelector;
		private readonly SignalBus _signalBus;

		[Inject]
		public WindowsService
		(
			WindowsChain windowsChain,
			IStorage storage,
			LanguageSelector languageSelector,
			SignalBus signalBus
		)
		{
			_windowsChain = windowsChain;

			_signalBus = signalBus;
			_languageSelector = languageSelector;
			_storage = storage;
		}

		public void OnVictory() => OpenResultWindowWith(SessionResult.Victory);

		public void OnLose() => OpenResultWindowWith(SessionResult.Lose);

		private void OpenResultWindowWith(SessionResult sessionResult)
			=> _windowsChain.Open<GameResultWindow>((w) => Initialize(w, sessionResult));

		private void Initialize(GameResultWindow window, SessionResult sessionResult)
			=> window.Construct(sessionResult);

		public void OpenSettings()
		{
			_windowsChain.Open<SettingsWindow>((w) => w.Construct(_storage, _languageSelector, _signalBus));
		}
	}
}