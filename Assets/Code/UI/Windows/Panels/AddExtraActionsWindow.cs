using Code.GameLoop;
using Code.Infrastructure.Configurations.Interfaces;
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
		private ActionsRemaining _actionsRemaining;

		[Inject]
		public void Construct
		(
			ICoinsConfig coinsConfig,
			WindowsChain windowsChain,
			WindowsService windowsService,
			ActionsRemaining actionsRemaining
		)
		{
			_coinsConfig = coinsConfig;
			_windowsChain = windowsChain;
			_windowsService = windowsService;
			_actionsRemaining = actionsRemaining;
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
		{
			_windowsChain.Open<ConfirmPurchaseWindow>((w) => w.Initialize(_coinsConfig.ExtraActionsPrice));
			_windowsChain.WindowClose += OnWindowClose;
		}

		private void OnWindowClose(WindowResult result)
		{
			if (result is WindowResult.Yes)
			{
				_actionsRemaining.BuyActions(_coinsConfig.ActionsCountPerPurchase);
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