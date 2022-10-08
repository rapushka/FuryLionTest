using Code.Audio;
using Code.Extensions;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
	public class AudioInstaller : MonoInstaller
	{
		[SerializeField] private AudioSource _musicSource;
		[SerializeField] private AudioSource _sfxSource;
		[SerializeField] private AudioCollection _audios;

		// ReSharper disable Unity.PerformanceAnalysis метод вызывается только на инициализации
		public override void InstallBindings()
		{
			Container
				.BindSingleFromInstance(new MusicAudioSource(_musicSource, _audios.Music))
				.BindSingleFromInstance(new SfxTokenAddedToChainAudioSource(_sfxSource))
				;
			
			SubscribeSignals();
		}

		private void SubscribeSignals()
		{
			Container
				.BindSignalTo<ChainTokenAddedSignal, SfxTokenAddedToChainAudioSource>((x) => x.PlayTokenAddedToChain)
				.BindSignalTo<ChainLastTokenRemovedSignal, SfxTokenAddedToChainAudioSource>((x) => x.DecreasePitch)
				.BindSignalTo<ChainEndedSignal, SfxTokenAddedToChainAudioSource>((x) => x.ResetPitch)
				;
		}
	}
}