using System;
using System.Collections;
using CodeBase.Infrastructure.Services.Audio;
using UnityEngine;

namespace Codebase.Hero
{
	public class HeroDeath : MonoBehaviour
	{
		[SerializeField] private float _delayBeforeRestart = 2.5f;
		[SerializeField] private HeroHealth _health;
		[SerializeField] private HeroAttack _attack;
		[SerializeField] private HeroRotate _rotate;
		[SerializeField] private HeroAnimator _animator;
		[SerializeField] private AudioSource _deathSx;

		private bool _isDead;
		public Action Happened;
		public Action Restart;
		private IAudioService _audioService;

		public void Construct(IAudioService audioService) =>
			_audioService = audioService;

		private void Start() =>
			_health.HealthChanged += HealthChanged;

		private void OnDestroy() =>
			_health.HealthChanged -= HealthChanged;

		private void HealthChanged()
		{
			if (!_isDead && _health.Current <= 0)
				Die();
		}

		private void Die()
		{
			_isDead = true;
			_attack.enabled = false;
			_rotate.enabled = false;
			
			_audioService.PlayDeathSound(_deathSx);
			_animator.PlayDeath();
			Happened?.Invoke();
			
			StartCoroutine(Death());
		}

		private IEnumerator Death()
		{
			yield return new WaitForSeconds(_delayBeforeRestart);
			Restart?.Invoke();
		}
	}
}