using System;
using Codebase.Hero;
using UnityEngine;

namespace Codebase.Enemy
{
	public class EnemyRotate : MonoBehaviour
	{
		private bool _isFacingRight;
		private Transform _heroTransform;
		private HeroDeath _heroDeath;
		private bool _isHeroAlive;

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
			if (_isHeroAlive)
			{
				if (_isFacingRight && _heroTransform.position.x > transform.position.x)
					Flip();
				else if (!_isFacingRight && _heroTransform.position.x < transform.position.x)
					Flip();
			}
		}

		private void Flip()
		{
			transform.Rotate(0, 180,0);
			_isFacingRight = !_isFacingRight;
		}
	}
}