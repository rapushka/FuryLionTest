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

		private ICoinsConfig _coinsConfig;

		[Inject] public void Construct(ICoinsConfig coinsConfig) => _coinsConfig = coinsConfig;

		public void Initialize(WindowsService windowsService) => _windowsService = windowsService;

		private void OnEnable()
		{
			_buttonYes.onClick.AddListener(OnButtonYesClick);
			_buttonNo.onClick.AddListener(Close);
		}

		private void OnDisable()
		{
			_buttonYes.onClick.RemoveListener(OnButtonYesClick);
			_buttonNo.onClick.RemoveListener(Close);
		}

		private void OnButtonYesClick() 
			=> _windowsService.ShowConfirmPurchaseWindow(_coinsConfig.ExtraActionsPrice, OnWindowClose);

		private void OnWindowClose(WindowResult result)
		{
			if (result is not WindowResult.Yes)
			{
				Close();
				return;
			}
			
			// TODO: Add extra actions
			Close();
		}

		private void Close() => _windowsService.Lose();
	}
}