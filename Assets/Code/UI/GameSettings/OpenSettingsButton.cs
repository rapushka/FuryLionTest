using Code.Generated.Analytics.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.GameSettings
{
	public class OpenSettingsButton : MonoBehaviour
	{
		[SerializeField] private Button _buttonComponent;

		private SettingsWindow _settingsWindow;
		private SignalBus _signalBus;

		[Inject]
		public void Construct(SettingsWindow settingsWindow, SignalBus signalBus)
		{
			_signalBus = signalBus;
			_settingsWindow = settingsWindow;
		}

		private void OnEnable() => _buttonComponent.onClick.AddListener(OpenSettingsWindow);

		private void OnDestroy() => _buttonComponent.onClick.RemoveListener(OpenSettingsWindow);

		private void OpenSettingsWindow()
		{
			_signalBus.Fire<SettingsOpenedSignal>();
			_settingsWindow.OpenWindow();
		}
	}
}