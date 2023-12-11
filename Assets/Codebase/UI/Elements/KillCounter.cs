using Codebase.Data;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements
{
  public class KillCounter : MonoBehaviour
  {
    public TextMeshProUGUI Counter;
    private KillData _killData;

    public void Construct(KillData killData)
    {
      _killData = killData;
      _killData.KilledMobsChanged += UpdateCounter;
    }

    private void Start() =>
      UpdateCounter();

    private void UpdateCounter() => 
      Counter.text = $"{_killData.KilledMobs}";
  }

}