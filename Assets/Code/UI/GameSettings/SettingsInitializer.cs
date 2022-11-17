using Zenject;

namespace Code.UI.GameSettings
{
	public class SettingsInitializer : IInitializable
	{
		private readonly SettingsWindow _settingsWindow;

		[Inject] public SettingsInitializer(SettingsWindow settingsWindow) => _settingsWindow = settingsWindow;

		public void Initialize()
		{
			// _settingsWindow.LoadSettings();
		}
	}
}