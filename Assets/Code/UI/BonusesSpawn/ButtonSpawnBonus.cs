using Code.Gameplay.TokensField.Bonuses;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.BonusesSpawn
{
	public abstract class ButtonSpawnBonus : MonoBehaviour
	{
		[SerializeField] private Button _button;
		
		protected BonusSpawner Spawner;

		[Inject] public void Construct(BonusSpawner spawner) => Spawner = spawner;
		
		private void OnEnable() => _button.onClick.AddListener(Spawn);
		
		private void OnDisable() => _button.onClick.RemoveListener(Spawn);
		
		protected abstract void Spawn();
	}
}