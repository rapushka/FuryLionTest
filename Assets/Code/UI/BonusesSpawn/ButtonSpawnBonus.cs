using Code.Gameplay.Coins;
using Code.Gameplay.TokensField.Bonuses;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.BonusesSpawn
{
	public abstract class ButtonSpawnBonus : MonoBehaviour
	{
		[SerializeField] private Button _button;
		
		protected Purchase Purchase;

		[Inject] public void Construct(Purchase purchase) => Purchase = purchase;
		
		private void OnEnable() => _button.onClick.AddListener(Spawn);
		
		private void OnDisable() => _button.onClick.RemoveListener(Spawn);
		
		protected abstract void Spawn();
	}
}