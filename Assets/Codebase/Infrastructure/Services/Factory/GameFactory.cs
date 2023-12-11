using System;
using System.Collections.Generic;
using Codebase.Enemy;
using Codebase.Hero;
using Codebase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.Audio;
using Codebase.Infrastructure.Services.Inputs;
using Codebase.Infrastructure.Services.PersistentProgress;
using Codebase.Infrastructure.Services.Randomizer;
using Codebase.Infrastructure.Services.StaticData;
using Codebase.Logic;
using Codebase.Logic.EnemySpawners;
using Codebase.StaticData;
using Codebase.UI.Elements;
using CodeBase.UI.Elements;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Codebase.Infrastructure.Services.Factory
{
	public class GameFactory : IGameFactory
	{
		public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
		public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

		private readonly IAssetProvider _assets;
		private readonly IStaticDataService _staticData;
		private readonly IRandomService _randomService;
		private readonly IInputService _inputService;
		private readonly IPersistentProgressService _progressService;
		private readonly IAudioService _audioService;

		public GameObject HeroGameObject { get; private set; }
		public List<SpawnPoint> Spawners { get; } = new List<SpawnPoint>();
		public List<GameObject> Monsters { get; } = new List<GameObject>();
		public Hud HUD { get; private set; }
		public Action<GameObject> MonsterCreated { get; set; }

		public GameFactory(IAssetProvider assets, IStaticDataService staticData, IRandomService randomService, IInputService inputService,
			IPersistentProgressService progressService, IAudioService audioService)
		{
			_assets = assets;
			_staticData = staticData;
			_randomService = randomService;
			_inputService = inputService;
			_progressService = progressService;
			_audioService = audioService;
		}

		public Hud CreateHud()
		{
			HUD = InstantiateRegistered(AssetPath.HudPath).GetComponent<Hud>();
			HUD.TryGetComponent(out KillCounter killCounter);
			killCounter.Construct(_progressService.Progress.KillData);
			return HUD;
		}

		public GameObject CreateHero(Vector3 at)
		{
			HeroGameObject = InstantiateRegistered(AssetPath.HeroPath, at);

			HeroGameObject.TryGetComponent(out IHealth health);
			health.Construct(_audioService);

			HeroGameObject.TryGetComponent(out HeroDeath heroDeath);
			heroDeath.Construct(_audioService);
			
			HeroGameObject.transform.GetChild(0).TryGetComponent(out HeroAttack heroAttack);
			heroAttack.Construct(_inputService, _audioService);

			HeroGameObject.TryGetComponent(out HeroRotate heroRotate);
			heroRotate.Construct(_inputService);

			return HeroGameObject;
		}

		public GameObject CreateMonster(EnemyTypeId typeId, Transform parent)
		{
			EnemyStaticData enemyData = _staticData.ForMonster(typeId);
			GameObject enemy = Object.Instantiate(enemyData.Prefab, parent.position, Quaternion.identity);

			HeroGameObject.TryGetComponent(out HeroDeath heroDeath);

			enemy.TryGetComponent(out IHealth health);
			health.Construct(_audioService);
			health.Current = enemyData.Hp;
			health.Max = enemyData.Hp;

			enemy.TryGetComponent(out AgentMoveToPlayer moveToPlayer);
			moveToPlayer.Construct(HeroGameObject.transform, heroDeath);
			moveToPlayer.MovementSpeed = enemyData.MoveSpeed;

			enemy.TryGetComponent(out AnimateAlongAgent animateAlongAgent);
			animateAlongAgent.Construct(HeroGameObject.transform, heroDeath);

			enemy.TryGetComponent(out EnemyRotate enemyRotate);
			enemyRotate.Construct(HeroGameObject.transform, heroDeath);

			enemy.transform.GetChild(0).TryGetComponent(out EnemyAttack enemyAttack);
			enemyAttack.Construct(heroDeath, _audioService);
			enemyAttack.Damage = enemyData.Damage;
			enemyAttack.Cleavage = enemyData.Cleavage;

			enemy.TryGetComponent(out EnemyDeath death);
			death.Construct(_progressService ,_audioService);
			
			Monsters.Add(enemy);
			MonsterCreated?.Invoke(enemy);
			return enemy;
		}

		public void CreateSpawner(string spawnerId, Vector3 at, List<EnemyTypeId> normalEnemies, List<EnemyTypeId> strongEnemies)
		{
			InstantiateRegistered(AssetPath.Spawner, at).TryGetComponent(out SpawnPoint spawner);
			HeroGameObject.TryGetComponent(out HeroDeath heroDeath);
			spawner.Construct(this, _randomService, heroDeath);
			spawner.Id = spawnerId;
			spawner.NormalEnemies = normalEnemies;
			spawner.StrongEnemies = strongEnemies;
			Spawners.Add(spawner);
		}

		public void Dispose()
		{
			foreach (GameObject monster in Monsters)
			{
				Object.Destroy(monster.gameObject);
			}

			foreach (SpawnPoint spawner in Spawners)
			{
				Object.Destroy(spawner.gameObject);
			}

			Object.Destroy(HeroGameObject);
			Monsters.Clear();
			Spawners.Clear();
		}

		private void Register(ISavedProgressReader progressReader)
		{
			if (progressReader is ISavedProgress progressWriter)
				ProgressWriters.Add(progressWriter);

			ProgressReaders.Add(progressReader);
		}

		public void Cleanup()
		{
			ProgressReaders.Clear();
			ProgressWriters.Clear();
		}

		private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
		{
			GameObject gameObject = _assets.Instantiate(path: prefabPath, at: at);
			RegisterProgressWatchers(gameObject);

			return gameObject;
		}

		private GameObject InstantiateRegistered(string prefabPath)
		{
			GameObject gameObject = _assets.Instantiate(path: prefabPath);
			RegisterProgressWatchers(gameObject);

			return gameObject;
		}

		private void RegisterProgressWatchers(GameObject gameObject)
		{
			foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
				Register(progressReader);
		}

	}

}