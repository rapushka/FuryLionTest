using Code.UI.Windows.Panels;
using UnityEngine;

namespace Code.GameLoop.Goals.Conditions
{
	public abstract class Goal : ScriptableObject
	{
		public abstract void Accept(IGoalVisitor goalVisitor);
	}
}