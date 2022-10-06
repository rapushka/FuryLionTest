using Code.Extensions;
using Code.Infrastructure.SceneManagement;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField] private SceneField _loseScene;
		[SerializeField] private SceneField _victoryScene;

		// ReSharper disable Unity.PerformanceAnalysis метод вызывается только на инициализации
		public override void InstallBindings()
		{
			var sceneTransfer = new SceneTransfer(_loseScene, _victoryScene);

			Container.BindSingleFromInstance(sceneTransfer);
		}
	}
}