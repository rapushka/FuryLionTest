using Code.Extensions;
using Code.Infrastructure.SceneManagement;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField] private SceneField _gameplayScene;
		[SerializeField] private SceneField _victoryScene;
		[SerializeField] private SceneField _loseScene;

		// ReSharper disable Unity.PerformanceAnalysis метод вызывается только на инициализации
		public override void InstallBindings()
		{
			var sceneTransfer = new SceneTransfer(_gameplayScene, _victoryScene, _loseScene);

			Container.BindSingleFromInstance(sceneTransfer);
			
			SignalBusInstaller.Install(Container);
		}
	}
}