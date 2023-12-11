using Codebase.Infrastructure.Services.Randomizer;
using Codebase.Infrastructure.Services.StaticData;
using Codebase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Audio
{
	public class AudioService : IAudioService
	{
		private readonly AudioSource _backgroundsMusicSource;
		private readonly IRandomService _randomService;
		private readonly SoundStaticData _soundData;

		private int _currentTrackIndex;

		public AudioService(IStaticDataService staticData, AudioSource backgroundsMusicSource, IRandomService randomService)
		{
			_soundData = staticData.ForSounds();
			_backgroundsMusicSource = backgroundsMusicSource;
			_randomService = randomService;
		}

		public void PlaySwordSound(AudioSource source)
		{
			int next = _randomService.Next(0, _soundData.SwordAttacksSFX.Count);
			source.PlayOneShot(_soundData.SwordAttacksSFX[next]);
		}	
		
		public void PlayMissSwordSound(AudioSource source)
		{
			int next = _randomService.Next(0, _soundData.MissSwordAttacksSFX.Count);
			source.PlayOneShot(_soundData.MissSwordAttacksSFX[next]);
		}		
		
		public void PlayHitSound(AudioSource source)
		{
			int next = _randomService.Next(0, _soundData.HurtSFX.Count);
			source.PlayOneShot(_soundData.HurtSFX[next]);
		}
		
		public void PlayDeathSound(AudioSource source) =>
			source.PlayOneShot(_soundData.HeroDeathSFX);

		public void Update()
		{
			if (_backgroundsMusicSource.isPlaying)
				return;

			_currentTrackIndex++;

			if (_currentTrackIndex > _soundData.MainThemes.Count)
				_currentTrackIndex = 0;

			Debug.Log(_currentTrackIndex);
			Debug.Log(_soundData.MainThemes.Count);
			_backgroundsMusicSource.clip = _soundData.MainThemes[_currentTrackIndex];
			_backgroundsMusicSource.Play();
		}


	}

}