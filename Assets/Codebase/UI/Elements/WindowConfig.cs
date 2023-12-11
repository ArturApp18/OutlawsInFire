using System;
using Codebase.UI.Menu;
using Codebase.UI.Services.Windows;
using Codebase.UI.Windows;

namespace Codebase.UI.Elements
{
  [Serializable]
  public class WindowConfig
  {
    public WindowId WindowId;
    public MainMenu MenuPrefab;
    public WindowBase Template;
  }
}