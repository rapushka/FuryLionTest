using Code.Infrastructure.ScenesTransfers;
using Code.Inner;
using Zenject;

namespace Code.Infrastructure.Bootstrap
{
	public class GameStarter : IInitializable
	{
		private readonly SceneTransfer _sceneTransfer;

		[Inject] public GameStarter(SceneTransfer sceneTransfer) => _sceneTransfer = sceneTransfer;

		public void Initialize() => StartGame();
		
		private void StartGame()
		{
			if (_sceneTransfer.CurrentSceneIndex != Constants.SceneIndex.Bootstrap)
			{
				_sceneTransfer.ToBootstrapScene();
			}
		}
	}
}