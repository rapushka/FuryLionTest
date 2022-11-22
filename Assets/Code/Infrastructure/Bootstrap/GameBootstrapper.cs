using Code.Infrastructure.ScenesTransfers;
using Zenject;

namespace Code.Infrastructure.Bootstrap
{
	public class GameBootstrapper : IInitializable
	{
		private readonly SceneTransfer _sceneTransfer;

		[Inject] public GameBootstrapper(SceneTransfer sceneTransfer) => _sceneTransfer = sceneTransfer;

		public void Initialize() => _sceneTransfer.ToGameplayScene();
	}
}