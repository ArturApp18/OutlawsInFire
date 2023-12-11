using System;
using System.Collections;
using System.Linq;
using Codebase.Hero;
using CodeBase.Infrastructure.Services.Audio;
using Codebase.Logic;
using UnityEngine;

namespace Codebase.Enemy
{
	public class EnemyAttack : MonoBehaviour
	{
		private const string PlayerLayer = "Player";

		[SerializeField] private EnemyHealth _enemyHealth;
		[SerializeField] private EnemyAnimator _animator;
		[SerializeField] private float _cooldown = 3.0f;
		[SerializeField] private float _delayBeforeAttack = 1;
		[SerializeField] private Transform _attackPoint;
		[SerializeField] private AudioSource _impactSFX;

		private Collider2D[] _hits = new Collider2D[1];
		private int _layerMask;
		private bool _attackIsActive;
		private float _attackCooldown;
		private bool _isAttacking;

		public float Damage = 10;
		public float Cleavage = 0.5f;
		private bool _isHeroAlive;
		private HeroDeath _heroDeath;
		private IAudioService _audioService;


		public void Construct(HeroDeath heroDeath, IAudioService audioService)
		{
			_heroDeath = heroDeath;
			_audioService = audioService;
			_heroDeath.Happened += Happened;
		}

		private void Start() =>
			_isHeroAlive = true;

		private void Happened() =>
			_isHeroAlive = false;

		private void Awake() =>
			_layerMask = 1 << LayerMask.NameToLayer(PlayerLayer);

		private void Update()
		{
			UpdateCooldown();

			if (CanAttack())
				StartCoroutine(StartAttack());
		}

		private void OnAttack()
		{
			if (Hit(out Collider2D hit))
			{
				_audioService.PlaySwordSound(_impactSFX);
				hit.transform.GetComponent<IHealth>().TakeDamage(Damage);
			}
		}
		
		private void OnAttackEnded()
		{
			_attackCooldown = _cooldown;
			_isAttacking = false;
		}

		public void DisableAttack() =>
			_attackIsActive = false;

		public void EnableAttack() =>
			_attackIsActive = true;

		private bool Hit(out Collider2D hit)
		{
			int hitAmount = Physics2D.OverlapCircleNonAlloc(_attackPoint.position, Cleavage, _hits, _layerMask);

			hit = _hits.FirstOrDefault();

			return hitAmount > 0;
		}

		private bool CanAttack() =>
			_attackIsActive && !_isAttacking && CooldownIsUp();

		private bool CooldownIsUp() =>
			_attackCooldown <= 0f;

		private void UpdateCooldown()
		{
			if (!CooldownIsUp())
				_attackCooldown -= Time.deltaTime;
		}

		private void OnDrawGizmos() =>
			Gizmos.DrawWireSphere(_attackPoint.position, Cleavage);

		private IEnumerator StartAttack()
		{
			yield return new WaitForSeconds(_delayBeforeAttack);
			if (_isHeroAlive)
			{
				_animator.PlayAttack();
				_isAttacking = true;
			}
		}
	}
}