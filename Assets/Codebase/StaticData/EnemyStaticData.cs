using UnityEngine;

namespace Codebase.StaticData
{
  [CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/Enemy")]
  public class EnemyStaticData : ScriptableObject
  {
    public EnemyTypeId EnemyTypeId;
    
    [Range(1,15)]
    public int Hp;
    
    [Range(1,10)]
    public float Damage;

    [Range(.5f,1)]
    public float Cleavage = .5f;

    [Range(0,10)]
    public float MoveSpeed = 1.5f;
    
    public GameObject Prefab;
  }

}