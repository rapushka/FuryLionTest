using System.Collections.Generic;
using Code.Environment;
using Code.Environment.GravityBehaviour;
using Code.Extensions;
using Code.Gameplay;
using Code.Input;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure
{
	public class LevelInstaller : MonoInstaller, IInitializable
	{
		[SerializeField] private LineRenderer _lineRenderer;
		[SerializeField] private OverlapMouse _overlapMouse;
		[SerializeField] private Field _field;
		[SerializeField] private LevelGenerator _levelGenerator;
		[SerializeField] private TokensCollection _tokens;
		[SerializeField] private TokensSpawner _spawner;
		[SerializeField] private GameBalance _balance;

		private ChainRenderer _chainRenderer;
		private Chain _chain;

		public override void InstallBindings()
		{
			Container.BindInterfacesTo<LevelInstaller>().FromInstance(this);
			InitializeSignals();

			var tokens = _tokens.InitializedDictionary();

			Container.Bind<GameBalance>().FromInstance(_balance).AsSingle().NonLazy();
			Container.Bind<Dictionary<TokenType, Token>>().FromInstance(tokens).AsSingle().NonLazy();
			Container.Bind<LineRenderer>().FromInstance(_lineRenderer).AsSingle().NonLazy();

			Container.Bind<Gravity>().AsSingle().NonLazy();
			Container.Bind<Chain>().AsSingle().NonLazy();
			Container.Bind<ChainRenderer>().AsSingle().NonLazy();

			Container.Bind<Field>().FromInstance(_field).AsSingle().NonLazy();
			Container.Bind<LevelGenerator>().FromInstance(_levelGenerator).AsSingle().NonLazy();
			Container.Bind<TokensSpawner>().FromInstance(_spawner).AsSingle().NonLazy();

			Container.Bind<OverlapMouse>().FromInstance(_overlapMouse).AsSingle().NonLazy();
		}

		private void InitializeSignals()
		{
			SignalBusInstaller.Install(Container);

			Container
				.BindSignalTo<MouseDownSignal, OverlapMouse>((x) => x.EnableOverlapping)
				.BindSignalTo<MouseUpSignal, OverlapMouse>((x) => x.DisableOverlapping)
				.BindSignalTo<MouseUpSignal, Chain>((x) => x.EndComposing)
				;
		}

		public void Initialize()
		{
			_chainRenderer = Container.Resolve<ChainRenderer>();
			_chain = Container.Resolve<Chain>();
			SubscribeEvents();
		}

		private void SubscribeEvents()
		{
			_overlapMouse.ClickOnToken += _chain.StartComposing;
			_overlapMouse.TokenHit += _chain.NextToken;

			_chain.TokenAdded += _chainRenderer.OnTokenAdded;
			_chain.LastTokenRemoved += _chainRenderer.OnLastTokenRemoved;
			_chain.ChainEnded += _chainRenderer.OnChainEnded;

			_chain.ChainEnded += _field.OnChainEnded;
		}

		private void OnDisable()
		{
			_overlapMouse.ClickOnToken -= _chain.StartComposing;
			_overlapMouse.TokenHit -= _chain.NextToken;

			_chain.TokenAdded -= _chainRenderer.OnTokenAdded;
			_chain.LastTokenRemoved -= _chainRenderer.OnLastTokenRemoved;
			_chain.ChainEnded -= _chainRenderer.OnChainEnded;

			_chain.ChainEnded -= _field.OnChainEnded;
		}
	}
}