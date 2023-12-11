using System.Collections.Generic;
using Codebase.StaticData;
using UnityEngine;

namespace Codebase.Logic.EnemySpawners
{
  public class SpawnMarker : MonoBehaviour
  {
    public List<EnemyTypeId> NormalEnemies;
    public List<EnemyTypeId> StrongEnemies;
  }
}