using System.Linq;
using Codebase.Logic;
using Codebase.Logic.EnemySpawners;
using Codebase.StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor
{
	[CustomEditor(typeof(LevelStaticData))]
	public class LevelStaticDataEditor : UnityEditor.Editor
	{
		private const string InitialPointTag = "PlayerInitialPoint";

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			LevelStaticData levelData = (LevelStaticData) target;

			if (GUILayout.Button("Collect"))
			{
				levelData.EnemySpawners = FindObjectsOfType<SpawnMarker>()
					.Select(x => new EnemySpawnerStaticData(x.GetComponent<UniqueId>().Id, x.NormalEnemies, x.StrongEnemies, x.transform.position, x.transform.rotation))
					.ToList();

				levelData.LevelKey = SceneManager.GetActiveScene().name;

				levelData.InitialHeroPosition = GameObject.FindWithTag(InitialPointTag).transform.position;
			}

			EditorUtility.SetDirty(target);
		}
	}
}