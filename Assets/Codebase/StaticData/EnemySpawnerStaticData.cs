using System;
using System.Collections.Generic;
using UnityEngine;

namespace Codebase.StaticData
{
	[Serializable]
	public class EnemySpawnerStaticData
	{
		public string Id;
		public List<EnemyTypeId> NormalEnemies;
		public List<EnemyTypeId> StrongEnemies;
		public Vector3 Position;
		public Quaternion Rotation;

		public EnemySpawnerStaticData(string id, List<EnemyTypeId> normalEnemies, List<EnemyTypeId> strongEnemies, Vector3 position, Quaternion rotation)
		{
			Id = id;
			NormalEnemies = normalEnemies;
			StrongEnemies = strongEnemies;
			Position = position;
			Rotation = rotation;
		}
	}
}