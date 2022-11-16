using Code.Infrastructure.ScenesTransfers;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Bootstrap
{
	public class GameBootstrapper : MonoBehaviour, IInitializable
	{
		private SceneTransfer _sceneTransfer;

		[Inject] public void Construct(SceneTransfer sceneTransfer) => _sceneTransfer = sceneTransfer;

		public void Initialize() => _sceneTransfer.ToGameplayScene();
	}
}