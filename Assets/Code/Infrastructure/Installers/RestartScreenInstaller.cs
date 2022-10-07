using Code.Extensions;
using Code.Infrastructure.SceneManagement;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
	public class RestartScreenInstaller : MonoInstaller
	{
		[SerializeField] private ResetButton _resetButton;

		public override void InstallBindings()
		{
			Container.BindInstance(_resetButton);

			Container.BindSignalTo<ResetButtonClickSignal, SceneTransfer>((x) => x.ToGameplayScene);
		}
	}
}