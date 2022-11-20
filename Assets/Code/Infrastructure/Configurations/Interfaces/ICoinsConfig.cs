namespace Code.Infrastructure.Configurations.Interfaces
{
	public interface ICoinsConfig
	{
		int CoinsPerToken { get; }
		
		int HorizontalRocketPrice { get; }
		
		int BombPrice { get; }
		
		int ExtraActionsPrice { get; }
		
		int ActionsCountPerPurchase { get; }
	}
}