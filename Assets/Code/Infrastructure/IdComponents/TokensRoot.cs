using UnityEngine;

namespace Code.Infrastructure.IdComponents
{
	public class TokensRoot
	{
		public Transform Transform { get; }

		public TokensRoot(Transform tokensRoot)
		{
			Transform = tokensRoot;
		}
	}
}