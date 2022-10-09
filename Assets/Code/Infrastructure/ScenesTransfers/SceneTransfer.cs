using Code.Inner;
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

		public void ToGameplayScene() => SceneManager.LoadScene(Constants.SceneIndex.Gameplay);

		public void ToLoseScene() => SceneManager.LoadScene(Constants.SceneIndex.Lose);

		public void ToVictoryScene() => SceneManager.LoadScene(Constants.SceneIndex.Victory);
	}
}