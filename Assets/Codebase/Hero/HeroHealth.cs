using System;
using Codebase.Data;
using CodeBase.Infrastructure.Services.Audio;
using Codebase.Infrastructure.Services.PersistentProgress;
using Codebase.Infrastructure.Services.Randomizer;
using Codebase.Logic;
using UnityEngine;

namespace Codebase.Hero
{
	public class HeroHealth : MonoBehaviour, IHealth, ISavedProgress
	{
		[SerializeField] private HeroAnimator _animator;
		[SerializeField] private AudioSource _hurtSfx;
		
		private Stats _stats;

		private IAudioService _audioService;

		public event Action HealthChanged;

		public float Current
		{
			get => _stats.CurrentHP;
			set
			{
				if (value != _stats.CurrentHP)
				{
					_stats.CurrentHP = value;

					HealthChanged?.Invoke();
				}
			}
		}

		public void Construct( IAudioService audioService)
		{
		
			_audioService = audioService;
		}

		public float Max
		{
			get => _stats.MaxHP;
			set => _stats.MaxHP = value;
		}

		public void LoadProgress(PlayerProgress progress)
		{
			_stats = progress.HeroStats;
			HealthChanged?.Invoke();
			
		}

		public void UpdateProgress(PlayerProgress progress)
		{
			progress.HeroStats.CurrentHP = Current;
			progress.HeroStats.MaxHP = Max;
		}

		public void TakeDamage(float damage)
		{
			if (Current <= 0)
				return;
			
			_audioService.PlayHitSound(_hurtSfx);
			Current -= damage;
			_animator.PlayHit();
		}

	}
}