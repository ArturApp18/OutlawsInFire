using System.Collections.Generic;
using UnityEngine;

namespace Codebase.StaticData.Windows
{
  [CreateAssetMenu(menuName = "StaticData/Window", fileName = "WindowStaticData")]
  public class WindowStaticData : ScriptableObject
  {
    public List<WindowConfig> Configs;
  }
}