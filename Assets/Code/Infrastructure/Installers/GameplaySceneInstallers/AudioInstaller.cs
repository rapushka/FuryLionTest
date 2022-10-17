using Code.Audio;
using Code.Extensions.DiContainerExtensions;
using Code.Infrastructure.Signals.Bonuses;
using Code.Infrastructure.Signals.Chain;
using Code.Infrastructure.Signals.Goals;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Code.Infrastructure.Installers.GameplaySceneInstallers
{
	public class AudioInstaller : MonoInstaller
	{
		[SerializeField] private AudioSource _musicSource;
		[SerializeField] private AudioSource _sfxSource;
		[SerializeField] private AudioCollection _audios;
		[SerializeField] private AudioMixer _audioMixer;

		// ReSharper disable Unity.PerformanceAnalysis метод вызывается только на инициализации
		public override void InstallBindings()
		{
			Container
				.BindSingleFromInstanceWithInterfaces(_audios)
				.BindSingleFromInstance(new MusicAudioSource(_musicSource, _audios.Music))
				.BindSingleFromInstance(new TokenAddedAudioPitch(_sfxSource, _audios.TokenAddedToChainSfxPitchStep))
				.BindSingleFromInstance(new TokenAddedAudioSource(_sfxSource, _audios.TokenAddedToChain))
				.BindSingleFromInstance(new SfxAudioSource(_sfxSource, _audios))
				.BindSingleFromInstance(_audioMixer)
				;
			
			SubscribeSignals();
		}

		private void SubscribeSignals()
		{
			Container
				.BindSignalTo<ChainTokenAddedSignal, TokenAddedAudioSource>((x) => x.Play)
				.BindSignalTo<ChainTokenAddedSignal, TokenAddedAudioPitch>((x) => x.IncreasePitch)
				.BindSignalTo<ChainLastTokenRemovedSignal, TokenAddedAudioPitch>((x) => x.DecreasePitch)
				.BindSignalTo<ChainEndedSignal, TokenAddedAudioPitch>((x) => x.ResetPitch)
				.BindSignalTo<ChainComposedSignal, SfxAudioSource>((x) => x.PlayChainComposed)
				.BindSignalTo<BonusSpawnedSignal, SfxAudioSource>((x) => x.PlayBonusSpawned)
				.BindSignalTo<ChainLastTokenRemovedSignal, SfxAudioSource>((x) => x.PlayTokenRemoved)
				.BindSignalTo<GoalReachedSignal, SfxAudioSource>((x) => x.PlayGoalCompleted)
				;
		}
	}
}