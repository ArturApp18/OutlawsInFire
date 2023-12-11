using System.Collections;
using System.Collections.Generic;
using Codebase.Data;
using Codebase.Enemy;
using Codebase.Hero;
using Codebase.Infrastructure.Services.Factory;
using Codebase.Infrastructure.Services.PersistentProgress;
using Codebase.Infrastructure.Services.Randomizer;
using Codebase.StaticData;
using UnityEngine;

namespace Codebase.Logic.EnemySpawners
{
	public class SpawnPoint : MonoBehaviour, ISavedProgress
	{
		[SerializeField] private float _delayBeforeFirstSpawn = 7;
		[SerializeField] private float _delayBetweenFirstSpawn = 1.5f;

		[SerializeField] private List<EnemyTypeId> _normalEnemies;
		[SerializeField] private List<EnemyTypeId> _strongEnemies;
		public string Id { get; set; }

		private IGameFactory _factory;
		private IRandomService _randomService;

		private EnemyDeath _enemyDeath;
		private HeroDeath _heroDeath;

		public bool _isActive;
		private Coroutine _spawnCoroutine;

		public List<EnemyTypeId> NormalEnemies
		{
			get
			{
				return _normalEnemies;
			}
			set
			{
				_normalEnemies = value;
			}
		}
		public List<EnemyTypeId> StrongEnemies
		{
			get
			{
				return _strongEnemies;
			}
			set
			{
				_strongEnemies = value;
			}
		}

		public void Construct(IGameFactory gameFactory, IRandomService randomService, HeroDeath heroDeath)
		{
			_factory = gameFactory;
			_randomService = randomService;
			_heroDeath = heroDeath;
			
			_heroDeath.Happened += Happened;
		}

		private void Happened()
		{
			_isActive = false;
			StopSpawn();
		}

		private void Start()
		{
			_isActive = true;
		}

		private void OnDestroy()
		{
			_isActive = false;
		}

		public void StartSpawnNormalMob()
		{
			_spawnCoroutine = StartCoroutine(SpawnMeleeMob());
		}

		public void StopSpawn()
		{
			if (_spawnCoroutine != null)
			{
				StopCoroutine(_spawnCoroutine);
				_spawnCoroutine = null;
			}
		}

		public void LoadProgress(PlayerProgress progress) { }

		public void UpdateProgress(PlayerProgress progress) { }

		private IEnumerator SpawnMeleeMob()
		{
			while (_isActive)
			{
				yield return new WaitForSeconds(_delayBeforeFirstSpawn);
				
				CreateMob(NormalEnemies[Next()]);
				yield return new WaitForSeconds(_delayBetweenFirstSpawn);
				CreateMob(NormalEnemies[Next()]);
				yield return new WaitForSeconds(_delayBetweenFirstSpawn);
				CreateMob(NormalEnemies[Next()]);

				_delayBeforeFirstSpawn *= 0.95f;
			}
		}

		private int Next()
		{
			int next = _randomService.Next(0, NormalEnemies.Count);
			return next;
		}

		private GameObject CreateMob(EnemyTypeId enemyTypeId)
		{
			GameObject monster = _factory.CreateMonster(enemyTypeId, transform);
			return monster;
		}
	}
}