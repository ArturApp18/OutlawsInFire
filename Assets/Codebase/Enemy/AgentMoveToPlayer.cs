using System;
using Codebase.Data;
using Codebase.Hero;
using Codebase.Infrastructure.Services.Factory;
using UnityEngine;

namespace Codebase.Enemy
{
	public class AgentMoveToPlayer : Follow
	{ 
		private const float MinimalDistance = 0.7f;
		
		[SerializeField] private Rigidbody2D _rigidbody;

		private Transform _heroTransform;
		private IGameFactory _gameFactory;

		private float _movementSpeed;
		private HeroDeath _heroDeath;
		private bool _isHeroAlive;

		public float MovementSpeed
		{
			get
			{
				return _movementSpeed;
			}
			set
			{
				_movementSpeed = value;
			}
		}

		public void Construct(Transform heroTransform, HeroDeath heroDeath)
		{
			_heroTransform = heroTransform;
			_heroDeath = heroDeath;
			
			_heroDeath.Happened += Happened;
		}

		private void Start() =>
			_isHeroAlive = true;

		private void Happened() =>
			_isHeroAlive = false;

		private void Update()
		{
			if (Initialized() && HeroNotReached() && _isHeroAlive)
				Move();
			else
				_rigidbody.velocity = Vector2.zero;
		}


		private void Move()
		{
			if (_heroTransform.position.x > transform.position.x)
			{
				_rigidbody.velocity = new Vector2(MovementSpeed, _rigidbody.velocity.y);
			}
			else if (_heroTransform.position.x < transform.position.x)
			{
				_rigidbody.velocity = new Vector2(-MovementSpeed, _rigidbody.velocity.y);
			}
		}


		private bool Initialized() =>
			_heroTransform != null;
		
		private bool HeroNotReached() =>
			transform.position.SqrMagnitudeTo(_heroTransform.position) >= MinimalDistance;

	}
}