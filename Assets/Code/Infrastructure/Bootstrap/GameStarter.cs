using Code.Infrastructure.ScenesTransfers;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Bootstrap
{
	public class GameStarter : MonoBehaviour, IInitializable
	{
		[SerializeField] private GameBootstrapper _bootstrapperPrefab;
		
		private SceneTransfer _sceneTransfer;

		[Inject] public void Construct(SceneTransfer sceneTransfer) => _sceneTransfer = sceneTransfer;

		public void Initialize()
		{
			if (FindObjectOfType<GameBootstrapper>() == null)
			{
				var bootstrapper = Instantiate(_bootstrapperPrefab);
				DontDestroyOnLoad(bootstrapper);
				_sceneTransfer.ToBootstrapScene();
			}
		}
	}
}