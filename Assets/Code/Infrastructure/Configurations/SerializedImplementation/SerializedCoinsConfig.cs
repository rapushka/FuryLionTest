using System;
using Code.Infrastructure.Configurations.Interfaces;
using UnityEngine;

namespace Code.Infrastructure.Configurations.SerializedImplementation
{
	[Serializable]
	public class SerializedCoinsConfig : ICoinsConfig
	{
		[field: SerializeField] public int CoinsPerToken { get; private set; } = 1;
	}
}