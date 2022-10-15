using Code.Extensions.DiContainerExtensions;
using Code.Infrastructure.ScenesTransfers;
using Code.Infrastructure.Signals.GameLoop;
using Code.UI;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
	public class VictoryScreenInstaller : MonoInstaller
	{
		[SerializeField] private ResetButton _resetButton;

		public override void InstallBindings()
		{
			Container.BindInstance(_resetButton);

			Container.BindSignalTo<ResetButtonClickSignal, SceneTransfer>((x) => x.ToGameplayScene);
		}
	}
}