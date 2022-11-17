using Code.Infrastructure.ScenesTransfers;
using Code.Inner;
using Zenject;

namespace Code.Infrastructure.Bootstrap
{
	public class GameStarter : IInitializable
	{
		private readonly SceneTransfer _sceneTransfer;

		[Inject] public GameStarter(SceneTransfer sceneTransfer) => _sceneTransfer = sceneTransfer;

		private bool IsDontOnBootstrapScene => _sceneTransfer.CurrentSceneIndex != Constants.SceneIndex.Bootstrap;
		
		public void Initialize() => StartGame();

		public void StartGame()
		{
			if (IsDontOnBootstrapScene)
			{
				_sceneTransfer.ToBootstrapScene();
			}
		}
	}
}