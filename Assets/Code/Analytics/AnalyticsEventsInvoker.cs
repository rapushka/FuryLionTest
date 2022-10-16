using Code.Generated.Analytics.Signals;
using Code.Infrastructure.ScenesTransfers;
using Code.Inner;
using Zenject;

namespace Code.Analytics
{
	public class AnalyticsEventsInvoker
	{
		private readonly SignalBus _signalBus;
		private readonly SceneTransfer _sceneTransfer;

		[Inject]
		public AnalyticsEventsInvoker(SignalBus signalBus, SceneTransfer sceneTransfer)
		{
			_signalBus = signalBus;
			_sceneTransfer = sceneTransfer;
		}
		
		public void OnSceneChanged()
		{
			if (_sceneTransfer.CurrentSceneIndex == Constants.SceneIndex.Gameplay)
			{
				_signalBus.Fire(new LevelOpenedSignal(Constants.SceneIndex.Gameplay));
			}
		}
	}
}