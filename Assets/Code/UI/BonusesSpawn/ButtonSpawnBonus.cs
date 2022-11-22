using Code.Gameplay.Coins;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.BonusesSpawn
{
	public abstract class ButtonSpawnBonus : MonoBehaviour
	{
		[SerializeField] private Button _button;
		
		protected PurchaseBonus PurchaseBonus;

		[Inject] public void Construct(PurchaseBonus purchaseBonus) => PurchaseBonus = purchaseBonus;
		
		private void OnEnable() => _button.onClick.AddListener(Spawn);
		
		private void OnDisable() => _button.onClick.RemoveListener(Spawn);
		
		protected abstract void Spawn();
	}
}