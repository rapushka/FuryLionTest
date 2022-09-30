using UnityEngine;

namespace Code.Infrastructure.Configurations
{
	public interface IFieldConfig
	{
		public float Step { get; }
		public Vector2 Offset { get; }
	}
}