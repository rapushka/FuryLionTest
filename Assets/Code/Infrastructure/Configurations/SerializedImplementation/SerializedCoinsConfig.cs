using System;
using Code.Infrastructure.Configurations.Interfaces;
using UnityEngine;

namespace Code.Infrastructure.Configurations.SerializedImplementation
{
	[Serializable]
	public class SerializedCoinsConfig : ICoinsConfig
	{
		[field: SerializeField] public int CoinsPerToken { get; private set; } = 1;

		[field: SerializeField] public int HorizontalRocketPrice { get; private set; } = 100;

		[field: SerializeField] public int BombPrice { get; private set; } = 250;

		[field: SerializeField] public int AdditionalActionsPrice { get; private set; } = 10;

		[field: SerializeField] public int ActionsCountPerPurchase { get; private set; } = 5;
	}
}