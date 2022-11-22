using System;
using Code.Infrastructure.Signals.GameLoop;
using Code.Inner;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Infrastructure.ScenesTransfers
{
	public class SceneTransfer : IInitializable, IDisposable
	{
		private readonly SignalBus _signalBus;

		[Inject] public SceneTransfer(SignalBus signalBus) => _signalBus = signalBus;

		public int CurrentSceneIndex => SceneManager.GetActiveScene().buildIndex;

		public void Initialize() => SceneManager.activeSceneChanged += OnSceneLoaded;

		public void Dispose() => SceneManager.activeSceneChanged -= OnSceneLoaded;

		private void OnSceneLoaded(Scene prevScene, Scene newScene) => _signalBus.Fire<SceneLoadedSignal>();

		public void ToGameplayScene() => SceneManager.LoadScene(Constants.SceneIndex.Gameplay);

		public void ToBootstrapScene() => SceneManager.LoadScene(Constants.SceneIndex.Bootstrap);
	}
}