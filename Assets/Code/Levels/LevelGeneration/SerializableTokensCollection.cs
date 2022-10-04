using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Levels.LevelGeneration
{
	[Serializable]
	public class SerializableTokensCollection
	{
		[SerializeField] private List<TokenToSpawnUnitEntry> _entries;

		public List<TokenToSpawnUnitEntry> Entries => _entries;
	}
}