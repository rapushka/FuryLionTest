using Code.UI.Windows.Panels;
using Zenject;

namespace Code.UI.Windows.Service
{
	public class WindowsService
	{
		private readonly WindowsChain _windowsChain;

		[Inject] public WindowsService(WindowsChain windowsChain) => _windowsChain = windowsChain;

		public void OnVictory() => OpenResultWindowWith(SessionResult.Victory);

		public void OnLose() => OpenResultWindowWith(SessionResult.Lose);

		private void OpenResultWindowWith(SessionResult sessionResult) 
			=> _windowsChain.Open<GameResultWindow>((w) => Initialize(w, sessionResult));

		private void Initialize(GameResultWindow window, SessionResult sessionResult) 
			=> window.Construct(sessionResult);
	}
}