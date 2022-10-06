using UnityEngine.SceneManagement;

namespace Code.Infrastructure.SceneManagement
{
	public class SceneTransfer
	{
		private readonly SceneField _gameplayScene;
		private readonly SceneField _loseScene;
		private readonly SceneField _victoryScene;

		public SceneTransfer(SceneField gameplayScene,  SceneField loseScene, SceneField victoryScene)
		{
			_gameplayScene = gameplayScene;
			_loseScene = loseScene;
			_victoryScene = victoryScene;
		}

		public void ToGameplayScene() => SceneManager.LoadScene(_gameplayScene);
		
		public void ToLoseScene() => SceneManager.LoadScene(_loseScene);

		public void ToVictoryScene() => SceneManager.LoadScene(_victoryScene);
	}
}