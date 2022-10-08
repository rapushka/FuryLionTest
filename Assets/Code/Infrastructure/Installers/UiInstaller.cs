using Code.Extensions;
using Code.UI.Settings;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Infrastructure.Installers
{
	public class UiInstaller : MonoInstaller
	{
		[SerializeField] private OpenSettingsButton _settingsButton;
		[SerializeField] private SettingsWindow _settings;

		public override void InstallBindings()
		{
			Container
				.BindSingleFromInstance(_settings)
				;
		}
	}
}