using System.Linq;
using Codebase.Data;
using CodeBase.Infrastructure.Services.Audio;
using Codebase.Infrastructure.Services.Inputs;
using Codebase.Infrastructure.Services.PersistentProgress;
using Codebase.Logic;
using UnityEngine;

namespace Codebase.Hero
{
	public class HeroAttack : MonoBehaviour, ISavedProgressReader
	{
		private const string Hittable = "Hittable";

		[SerializeField] private AudioSource _impactSx;
		[SerializeField] private HeroAnimator _animator;
		[SerializeField] private Transform _attackPoint;

		private GameObject _impactVfx;
		private Collider2D[] _hits = new Collider2D[1];
		private float _attackButtonPressedTimer;
		private Stats _stats;
		private int _layerMask;

		private IInputService _inputService;
		private IAudioService _audioService;
		private bool _isAttacking;
		private bool _isHeroAlive;

		public void Construct(IInputService inputService, IAudioService audioService)
		{
			_inputService = inputService;
			_audioService = audioService;
			_layerMask = 1 << LayerMask.NameToLayer(Hittable);
		}

		private void Update()
		{
			if (!_inputService.IsLeftAttackButtonDown() && !_inputService.IsRightAttackButtonDown())
				return;

			_animator.AttackCounter++;

			if (_animator.AttackCounter > 3)
				_animator.AttackCounter = 0;

			_animator.PlayAttack();
	
		}

		public void LoadProgress(PlayerProgress progress) =>
			_stats = progress.HeroStats;


		private void OnAttack()
		{
			_isAttacking = true;
			PhysicsDebug.DrawDebug(_attackPoint.position, _stats.AttackRadius, 1.0f);

			if (Hit(out Collider2D hit))
			{
				_audioService.PlaySwordSound(_impactSx);
				hit.transform.GetComponentInParent<IHealth>().TakeDamage(_stats.Damage);
			}
			else
			{
				_audioService.PlayMissSwordSound(_impactSx);
			}
			
		}

		private void OnAttackEnded() =>
			_isAttacking = false;

		private bool Hit(out Collider2D hit)
		{
			int hitAmount = Physics2D.OverlapCircleNonAlloc(_attackPoint.position, _stats.AttackRadius, _hits, _layerMask);

			hit = _hits.FirstOrDefault();

			return hitAmount > 0;
		}
	}
}