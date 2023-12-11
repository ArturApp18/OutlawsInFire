using System;
using Codebase.Data;
using Codebase.Hero;
using UnityEngine;
using UnityEngine.AI;

namespace Codebase.Enemy
{
  public class AnimateAlongAgent : MonoBehaviour
  {
    private const float MinimalVelocity = 0.1f;
    private const float MinimalDistance = 1.5f;

    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private EnemyAnimator Animator;
    
    private Transform _heroTransform;
    private HeroDeath _heroDeath;
    private bool _isHeroAlive;

    public void Construct(Transform heroTransform, HeroDeath heroDeath)
    {
      _heroTransform = heroTransform;
      _heroDeath = heroDeath;
      
      heroDeath.Happened += Happened;
    }

    private void Start() =>
      _isHeroAlive = true;

    private void Happened() =>
      _isHeroAlive = false;

    private void Update()
    {
      if(ShouldMove() && _isHeroAlive)
        Animator.Move();
      else 
        Animator.StopMoving();
    }

    private bool ShouldMove() => 
      _rigidbody2D.velocity.magnitude > MinimalVelocity && HeroNotReached();
    
    private bool HeroNotReached() =>
      transform.position.SqrMagnitudeTo(_heroTransform.position) >= MinimalDistance;
  }
}