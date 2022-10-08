using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Settings
{
	public class OpenSettingsButton : MonoBehaviour
	{
		[SerializeField] private Button _buttonComponent;

		private SettingsWindow _settingsWindow;

		[Inject]
		public void Construct(SettingsWindow settingsWindow)
		{
			_settingsWindow = settingsWindow;
		}

		private void OnEnable() => _buttonComponent.onClick.AddListener(_settingsWindow.OpenWindow);

		private void OnDestroy() => _buttonComponent.onClick.RemoveListener(_settingsWindow.OpenWindow);
	}
}