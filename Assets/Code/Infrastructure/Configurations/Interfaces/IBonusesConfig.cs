namespace Code.Infrastructure.Configurations.Interfaces
{
	public interface IBonusesConfig
	{
		int MinChainLenghtForRocket { get; }
		
		int MaxChainLenghtForRocket { get; }
		
		int MinChainLenghtForBomb { get; }

		int BombExplosionRange { get; }
	}
}