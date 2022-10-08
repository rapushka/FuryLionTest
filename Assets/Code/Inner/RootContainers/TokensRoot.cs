using UnityEngine;

namespace Code.Inner.RootContainers
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