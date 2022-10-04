using System;
using System.Collections.Generic;
using Code.GameCycle.VictoryConditions.Conditions;
using UnityEngine;

namespace Code.GameCycle.VictoryConditions
{
	[Serializable]
	public class VictoryConditionsCollection
	{
		[SerializeField] private List<VictoryCondition> _conditions;
	}
}