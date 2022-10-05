using UnityEngine.SceneManagement;

namespace Code.Infrastructure.SceneManagement
{
	public class SceneTransfer
	{
		private readonly SceneField _loseScene;
		private readonly SceneField _victoryScene;

		public SceneTransfer(SceneField loseScene, SceneField victoryScene)
		{
			_loseScene = loseScene;
			_victoryScene = victoryScene;
		}

		public void ToLoseScene() => SceneManager.LoadScene(_loseScene);

		public void ToVictoryScene() => SceneManager.LoadScene(_victoryScene);
	}
}