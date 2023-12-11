using System;
using Codebase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Audio;
using Codebase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Codebase.Enemy
{
	public class EnemyDeath : MonoBehaviour
	{
		[SerializeField] private Collider2D _hitBox;
		[SerializeField] private EnemyHealth _health;
		[SerializeField] private AgentMoveToPlayer _move;
		[SerializeField] private CheckAttackRange _range;
		[SerializeField] private EnemyAttack _enemyAttack;
		[SerializeField] private EnemyAnimator _animator;
		[SerializeField] private AudioSource _deathSx;
		[SerializeField] private float _destroyTimer = 1.5f;

		private IPersistentProgressService _progressService;
		private IAudioService _audioService;

		public event Action Happened;

		public void Construct(IPersistentProgressService progressService, IAudioService audioService)
		{
			_progressService = progressService;
			_audioService = audioService;
		}

		private void Start()
		{
			_health.HealthChanged += OnHealthChanged;
		}

		private void OnDestroy() =>
			_health.HealthChanged -= OnHealthChanged;

		private void OnHealthChanged()
		{
			if (_health.Current <= 0)
				Die(_destroyTimer);
		}

		private void Die(float destroyTimer)
		{
			_hitBox.enabled = false;
			_range.enabled = false;
			_enemyAttack.enabled = false;
			_move.enabled = false;
			
			_health.HealthChanged -= OnHealthChanged;
			
			_audioService.PlayDeathSound(_deathSx);
			_animator.PlayDeath();
			
			RegisterKilledMobs();

			Destroy(gameObject, destroyTimer);

			Happened?.Invoke();
		}

		private void RegisterKilledMobs() =>
			_progressService.Progress.KillData.Add(1);
	}
}