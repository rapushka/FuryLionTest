using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.GameSettings
{
	public class OpenSettingsButton : MonoBehaviour
	{
		[SerializeField] private Button _buttonComponent;

		private SettingsWindow _settingsWindow;

		[Inject] public void Construct(SettingsWindow settingsWindow) => _settingsWindow = settingsWindow;

		private void OnEnable() => _buttonComponent.onClick.AddListener(OpenWindow);

		private void OnDestroy() => _buttonComponent.onClick.RemoveListener(OpenWindow);

		private void OpenWindow() => _settingsWindow.OpenWindow();
	}
}