using System;
using Code.Gameplay.TokensField.Bonuses;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.BonusesSpawn
{
	public class SpawnBonusButton : MonoBehaviour
	{
		[SerializeField] private Button _button;

		private BonusSpawner _spawner;

		[Inject] public void Construct(BonusSpawner spawner) => _spawner = spawner;

		private void OnEnable() => _button.onClick.AddListener(Spawn);
		
		private void OnDisable() => _button.onClick.RemoveListener(Spawn);

		private void Spawn() => _spawner.SpawnHorizontalRocket();
	}
}