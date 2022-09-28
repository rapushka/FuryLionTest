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
		[SerializeField] private Field _field;
		[SerializeField] private LevelGenerator _levelGenerator;
		[SerializeField] private TokensCollection _tokens;
		[SerializeField] private TokensSpawner _spawner;
		[SerializeField] private GameBalance _balance;

		private ChainRenderer _chainRenderer;
		private Chain _chain;

		// ReSharper disable Unity.PerformanceAnalysis метод вызывается только на инициализации
		public override void InstallBindings()
		{
			Container.BindInterfacesTo<LevelInstaller>().FromInstance(this);
			InitializeSignals();

			var dictionaryTokensToType = _tokens.InitializedDictionary();

			Container
				.BindSingleFromInstance(_balance)
				.BindSingleFromInstance(dictionaryTokensToType)
				.BindSingleFromInstance(_lineRenderer)
				.BindSingle<Gravity>()
				.BindSingle<Chain>()
				.BindSingle<ChainRenderer>()
				.BindSingleWithInterfaces<OverlapMouse>()
				.BindSingleFromInstance(_field)
				.BindSingleFromInstance(_levelGenerator)
				.BindSingleFromInstance(_spawner)
				;
		}

		private void InitializeSignals()
		{
			SignalBusInstaller.Install(Container);

			Container
				.BindSignalTo<MouseDownSignal, OverlapMouse>((x) => x.EnableOverlapping)
				.BindSignalTo<MouseUpSignal, OverlapMouse>((x) => x.DisableOverlapping)
				.BindSignalTo<MouseUpSignal, Chain>((x) => x.EndComposing)
				.BindSignalTo<TokenHitSignal, Chain>((x, v) => x.NextToken(v.Value))
				.BindSignalTo<TokenClickSignal, Chain>((x, v) => x.StartComposing(v.Value))
				.BindSignalTo<ChainTokenAddedSignal, ChainRenderer>((x, v) => x.OnTokenAdded(v.Value))
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
			_chain.LastTokenRemoved += _chainRenderer.OnLastTokenRemoved;
			_chain.ChainEnded += _chainRenderer.OnChainEnded;

			_chain.ChainEnded += _field.OnChainEnded;
		}

		private void OnDisable()
		{
			_chain.LastTokenRemoved -= _chainRenderer.OnLastTokenRemoved;
			_chain.ChainEnded -= _chainRenderer.OnChainEnded;

			_chain.ChainEnded -= _field.OnChainEnded;
		}
	}
}