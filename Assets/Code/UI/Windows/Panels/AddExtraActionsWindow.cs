using Code.Infrastructure.Configurations.Interfaces;
using Code.Infrastructure.Signals.Coins;
using Code.UI.Windows.Service;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Windows.Panels
{
	public class AddExtraActionsWindow : UnityWindow
	{
		[SerializeField] private Button _buttonYes;
		[SerializeField] private Button _buttonNo;

		private WindowsService _windowsService;
		private WindowsChain _windowsChain;
		private ICoinsConfig _coinsConfig;
		private SignalBus _signalBus;

		[Inject]
		public void Construct
		(
			ICoinsConfig coinsConfig,
			WindowsChain windowsChain,
			WindowsService windowsService,
			SignalBus signalBus
		)
		{
			_coinsConfig = coinsConfig;
			_windowsChain = windowsChain;
			_windowsService = windowsService;
			_signalBus = signalBus;
		}

		private void OnEnable()
		{
			_buttonYes.onClick.AddListener(OnButtonYesClick);
			_buttonNo.onClick.AddListener(Lose);
		}

		private void OnDisable()
		{
			_buttonYes.onClick.RemoveListener(OnButtonYesClick);
			_buttonNo.onClick.RemoveListener(Lose);
		}

		private void OnButtonYesClick()
			=> _windowsChain.Open<ConfirmPurchaseWindow>(InitializePurchaseWindow, OnWindowClose);

		private void InitializePurchaseWindow(ConfirmPurchaseWindow window)
			=> window.Initialize(_coinsConfig.ExtraActionsPrice);

		private void OnWindowClose(WindowResult result)
		{
			if (result is WindowResult.Yes)
			{
				Result = WindowResult.Yes;
				_signalBus.Fire(new ActionsBoughtSignal(_coinsConfig.ActionsCountPerPurchase));
				_windowsChain.Close();
			}
			else
			{
				Lose();
			}
		}

		private void Lose() => _windowsService.Lose();
	}
}