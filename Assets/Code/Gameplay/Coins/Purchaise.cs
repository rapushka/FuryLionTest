using Code.Infrastructure.Configurations.Interfaces;
using Code.UI.Windows.Service;
using Zenject;

namespace Code.Gameplay.Coins
{
	public class Purchase
	{
		private readonly ICoinsConfig _coinsConfig;
		private readonly WindowsService _windowsService;

		[Inject]
		public Purchase
		(
			ICoinsConfig coinsConfig,
			WindowsService windowsService
		)
		{
			_coinsConfig = coinsConfig;
			_windowsService = windowsService;
		}

		public void BuyHorizontalRocket() => BuyBonus(_coinsConfig.HorizontalRocketPrice);

		public void BuyBomb() => BuyBonus(_coinsConfig.BombPrice);

		private void BuyBonus(int price) => _windowsService.ShowConfirmPurchaseWindow(price);
	}
}