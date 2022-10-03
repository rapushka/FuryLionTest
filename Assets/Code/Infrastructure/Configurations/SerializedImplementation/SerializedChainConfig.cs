using System;
using Code.Infrastructure.Configurations.Interfaces;
using UnityEngine;

namespace Code.Infrastructure.Configurations.SerializedImplementation
{
	[Serializable]
	public class SerializedChainConfig : IChainConfig
	{
		[SerializeField] private int _minTokensCountForChain = 3;

		public int MinTokensCountForChain => _minTokensCountForChain;
	}
}