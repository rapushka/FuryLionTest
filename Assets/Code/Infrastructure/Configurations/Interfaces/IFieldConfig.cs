using UnityEngine;

namespace Code.Infrastructure.Configurations.Interfaces
{
	public interface IFieldConfig
	{
		public float Step { get; }
		public Vector2 Offset { get; }
	}
}