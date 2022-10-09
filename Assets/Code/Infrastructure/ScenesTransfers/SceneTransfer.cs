using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.ScenesTransfers
{
	public class SceneTransfer
	{
		private readonly SceneField _gameplayScene;
		private readonly SceneField _loseScene;
		private readonly SceneField _victoryScene;

		public SceneTransfer(SceneField gameplayScene, SceneField victoryScene, SceneField loseScene)
		{
			_gameplayScene = gameplayScene;
			_loseScene = loseScene;
			_victoryScene = victoryScene;
		}

		public void ToGameplayScene() => SceneManager.LoadScene(0);

		public void ToLoseScene() => SceneManager.LoadScene(1);

		public void ToVictoryScene() => SceneManager.LoadScene(2);
	}
}