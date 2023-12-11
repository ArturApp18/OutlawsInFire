using Codebase.Logic;
using UnityEngine;

namespace Codebase.Enemy
{
  [RequireComponent(typeof(EnemyAttack))]
  public class CheckAttackRange : MonoBehaviour
  {
    [SerializeField] private EnemyAttack _enemyEnemyAttack;
    [SerializeField] private TriggerObserver _triggerObserver;

    private void Start()
    {
      _triggerObserver.TriggerEnter += TriggerEnter;
      _triggerObserver.TriggerExit += TriggerExit;
      
      _enemyEnemyAttack.DisableAttack();
    }

    private void OnDestroy()
    {
      _triggerObserver.TriggerEnter -= TriggerEnter;
      _triggerObserver.TriggerExit -= TriggerExit;
    }
    
    private void TriggerExit(Collider2D obj) =>
      _enemyEnemyAttack.DisableAttack();

    private void TriggerEnter(Collider2D obj) =>
      _enemyEnemyAttack.EnableAttack();
  }
}