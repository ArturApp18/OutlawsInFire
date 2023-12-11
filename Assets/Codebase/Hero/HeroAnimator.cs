using System;
using Codebase.Logic;
using UnityEngine;

namespace Codebase.Hero
{
	public class HeroAnimator : MonoBehaviour, IAnimationStateReader
	{
		[SerializeField] public Animator _animator;

		private static readonly int Attack1Hash = Animator.StringToHash("Attack1");
		private static readonly int Attack2Hash = Animator.StringToHash("Attack2");
		private static readonly int Attack3Hash = Animator.StringToHash("Attack3");
		private static readonly int HitHash = Animator.StringToHash("Hit");
		private static readonly int DieHash = Animator.StringToHash("Die");

		private readonly int _idleStateHash = Animator.StringToHash("Idle");
		private readonly int _attack1StateHash = Animator.StringToHash("Attack1");
		private readonly int _attack2StateHash = Animator.StringToHash("Attack2");
		private readonly int _attack3StateHash = Animator.StringToHash("Attack3");
		private readonly int _hitStateHash = Animator.StringToHash("Hit");
		private readonly int _deathStateHash = Animator.StringToHash("Death");

		public event Action<AnimatorState> StateEntered;
		public event Action<AnimatorState> StateExited;

		public int AttackCounter { get; set; }
		public AnimatorState State { get; private set; }
		public bool IsAttacking => State == AnimatorState.Attack;


		public void PlayHit() =>
			_animator.SetTrigger(HitHash);

		public void PlayAttack()
		{
			switch (AttackCounter)
			{
				case 1:
					_animator.SetTrigger(Attack1Hash);
					break;
				case 2:
					_animator.SetTrigger(Attack2Hash);
					break;
				case 3:
					_animator.SetTrigger(Attack3Hash);
					break;
			}
		}

		public void PlayDeath() =>
			_animator.SetTrigger(DieHash);


		public void EnteredState(int stateHash)
		{
			State = StateFor(stateHash);
			StateEntered?.Invoke(State);
		}

		public void ExitedState(int stateHash) =>
			StateExited?.Invoke(StateFor(stateHash));

		private AnimatorState StateFor(int stateHash)
		{
			AnimatorState state;
			if (stateHash == _idleStateHash)
			{
				state = AnimatorState.Idle;
			}
			else if (stateHash == _attack1StateHash)
			{
				state = AnimatorState.Attack;
			}
			else if (stateHash == _attack2StateHash)
			{
				state = AnimatorState.Attack;
			}
			else if (stateHash == _attack3StateHash)
			{
				state = AnimatorState.Attack;
			}
			else if (stateHash == _deathStateHash)
			{
				state = AnimatorState.Died;
			}
			else if (stateHash == _hitStateHash)
			{
				state = AnimatorState.Hit;
			}
			else
			{
				state = AnimatorState.Unknown;
			}

			return state;
		}

	}
}