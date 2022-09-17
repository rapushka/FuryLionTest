using System.Collections.Generic;
using System.Linq;
using Code.Workflow.Extensions;
using UnityEngine;

namespace Code.Environment
{
	public class Field : MonoBehaviour
	{
		[SerializeField] private Token.Token[] _tokens;

		private List<Token.Token> _addedTokens;
		private LineRenderer _lineRenderer;
		private bool _chainStarted;

		public void Construct(LineRenderer lineRenderer)
		{
			_lineRenderer = lineRenderer;
			_addedTokens = new List<Token.Token>();
		}

		public void StartChain(Vector2 position)
		{
			_chainStarted = true;
			AddTokenAt(position);
		}

		public void AddTokenToChain(Vector2 position) => position.Do(AddTokenAt, @if: TokenCanBeAdded);

		public void EndChain()
		{
			_lineRenderer.ClearPositions();
			_addedTokens.Clear();
			_chainStarted = false;
		}

		private void AddTokenAt(Vector2 position)
			=> position
			   .Do(_lineRenderer.AddPosition)
			   .Select(GetTokenByPosition)
			   .Do(_addedTokens.Add);

		private bool TokenCanBeAdded(Vector2 position)
			=> _chainStarted && TokenNotYetAdded(position) && TokenIsFittingType(position);

		private bool TokenNotYetAdded(Vector2 position)
			=> _addedTokens.Contains(GetTokenByPosition(position)) == false;

		private bool TokenIsFittingType(Vector2 position)
			=> GetTokenByPosition(position).TokenType == _addedTokens.First().TokenType;

		private Token.Token GetTokenByPosition(Vector2 position)
			=> _tokens.First((token) => (Vector2)token.transform.position == position);
	}
}