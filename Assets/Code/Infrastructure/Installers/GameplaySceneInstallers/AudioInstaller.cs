using Code.Audio;
using Code.Extensions;
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
				.BindSingleFromInstance(new SfiAudioSource(_sfxSource, _audios))
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
				.BindSignalTo<ChainComposedSignal, SfiAudioSource>((x) => x.PlayChainComposed)
				.BindSignalTo<BonusSpawnedSignal, SfiAudioSource>((x) => x.PlayBonusSpawned)
				.BindSignalTo<ChainLastTokenRemovedSignal, SfiAudioSource>((x) => x.PlayTokenRemoved)
				.BindSignalTo<GoalReachedSignal, SfiAudioSource>((x) => x.PlayGoalCompleted)
				;
		}
	}
}