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

		// ReSharper disable Unity.PerformanceAnalysis метод вызывается только на инициализации
		public override void InstallBindings()
		{
			Container
				.BindSingleFromInstance(new MusicAudioSource(_musicSource))
				.BindSingleFromInstance(new SfxAudioSource(_sfxSource))
				;
			
			SubscribeSignals();
		}

		private void SubscribeSignals()
		{
			Container
				.BindSignalTo<ChainTokenAddedSignal, SfxAudioSource>((x) => x.PlayTokenAddedToChain)
				;
		}
	}
}