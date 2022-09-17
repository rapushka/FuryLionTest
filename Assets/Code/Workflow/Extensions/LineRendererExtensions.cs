using UnityEngine;

namespace Code.Workflow.Extensions
{
	public static class LineRendererExtensions
	{
		public static void Add(this LineRenderer @this, Vector3 position)
		{
			@this.positionCount++;
			var index = @this.positionCount - 1;
			@this.SetPosition(index, position);			
		}
	}
}