using Codebase.Infrastructure.Services;
using Codebase.UI.Menu;

namespace Codebase.UI.Services.Factory
{
  public interface IUIFactory: IService
  {
    ScoreMenu CreateScoreMenu();
    void CreateUIRoot();
    MainMenu CreateMainMenu();
  }
}