using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Infrastructure.SceneManagement
{
	public class SceneTransfer
	{
		private readonly SceneField _loseScene;

		[Inject] public SceneTransfer(SceneField loseScene) => _loseScene = loseScene;

		public void ToLoseScene() => SceneManager.LoadScene(_loseScene);
	}
}