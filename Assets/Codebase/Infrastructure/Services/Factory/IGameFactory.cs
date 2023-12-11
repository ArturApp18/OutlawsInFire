using System;
using System.Collections.Generic;
using Codebase.Infrastructure.Services.PersistentProgress;
using Codebase.Logic.EnemySpawners;
using Codebase.StaticData;
using Codebase.UI.Elements;
using UnityEngine;

namespace Codebase.Infrastructure.Services.Factory
{
	public interface IGameFactory : IService, IDisposable
	{
		List<ISavedProgressReader> ProgressReaders { get; }
		List<ISavedProgress> ProgressWriters { get; }
		GameObject CreateHero(Vector3 at);
		GameObject CreateMonster(EnemyTypeId typeId, Transform parent);
		void Cleanup();

		GameObject HeroGameObject { get; }
		List<SpawnPoint> Spawners { get; }
		List<GameObject> Monsters { get; }
		Hud CreateHud();
		void CreateSpawner(string spawnerId, Vector3 at, List<EnemyTypeId> normalEnemies, List<EnemyTypeId> strongEnemies);
	}

}