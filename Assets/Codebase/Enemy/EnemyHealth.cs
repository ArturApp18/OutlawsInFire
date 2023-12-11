using System;
using CodeBase.Infrastructure.Services.Audio;
using Codebase.Logic;
using UnityEngine;

namespace Codebase.Enemy
{

	public class EnemyHealth : MonoBehaviour, IHealth
	{
		[SerializeField] private EnemyAnimator _animator;
		[SerializeField] private AudioSource _hurtSfx;
		[SerializeField] private float _current;
		[SerializeField] private float _max;
		
		private IAudioService _audioService;

		public event Action HealthChanged;

		public float Current
		{
			get => _current;
			set => _current = value;
		}

		public float Max
		{
			get => _max;
			set => _max = value;
		}

		public void Construct(IAudioService audioService)
		{
			_audioService = audioService;
		}

		public void TakeDamage(float damage)
		{
			Current -= damage;

			_audioService.PlayHitSound(_hurtSfx);
			_animator.PlayHit();

			HealthChanged?.Invoke();
		}

		public void LevelUp() { }

	}

}