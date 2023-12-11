using Codebase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Audio
{
	public interface IAudioService : IService
	{
		void Update();
		void PlaySwordSound(AudioSource source);
		void PlayHitSound(AudioSource source);
		void PlayDeathSound(AudioSource source);
		void PlayMissSwordSound(AudioSource source);
	}
}