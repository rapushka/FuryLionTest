using Code.Inner;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.ScenesTransfers
{
	public class SceneTransfer
	{
		public void ToGameplayScene() => SceneManager.LoadScene(Constants.SceneIndex.Gameplay);

		public void ToLoseScene() => SceneManager.LoadScene(Constants.SceneIndex.Lose);

		public void ToVictoryScene() => SceneManager.LoadScene(Constants.SceneIndex.Victory);
	}
}