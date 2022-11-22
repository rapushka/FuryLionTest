namespace Code.UI.BonusesSpawn
{
	public class ButtonSpawnBomb : ButtonSpawnBonus
	{
		protected override void Spawn() => PurchaseBonus.BuyBomb();
	}
}