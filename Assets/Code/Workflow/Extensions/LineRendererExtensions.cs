using UnityEngine;

namespace Code.Workflow.Extensions
{
	public static class LineRendererExtensions
	{
		public static void AddPosition(this LineRenderer @this, Vector2 position)
		{
			@this.positionCount++;
			var index = @this.positionCount - 1;
			@this.SetPosition(index, position);		
		}
		
		public static void RemoveLastPosition(this LineRenderer @this)
		{
			if (@this.positionCount > 0)
			{
				@this.positionCount--;
			}
		}

		public static void ClearPositions(this LineRenderer @this) => @this.positionCount = 0;
	}
}