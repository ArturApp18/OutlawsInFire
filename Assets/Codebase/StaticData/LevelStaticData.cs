using System.Collections.Generic;
using UnityEngine;

namespace Codebase.StaticData
{
  [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
  public class LevelStaticData : ScriptableObject
  {
    public string LevelKey;
    public List<EnemySpawnerStaticData> EnemySpawners;
    public Vector3 InitialHeroPosition;
  }

}