using UnityEngine;
using Zenject;

namespace Code.UI.GoalViews
{
	public class GoalsRoot
	{
		public Transform Transform { get; }

		[Inject] public GoalsRoot(Transform transform) => Transform = transform;
	}
}