using UnityEngine;
using Zenject;

namespace Code.Inner.RootContainers
{
	public class GoalsRoot
	{
		public Transform Transform { get; }

		[Inject] public GoalsRoot(Transform transform) => Transform = transform;
	}
}