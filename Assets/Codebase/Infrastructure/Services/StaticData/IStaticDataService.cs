using Codebase.StaticData;
using Codebase.StaticData.Windows;
using Codebase.UI.Services.Windows;

namespace Codebase.Infrastructure.Services.StaticData
{
  public interface IStaticDataService : IService
  {
    void Load();
    EnemyStaticData ForMonster(EnemyTypeId typeId);
    LevelStaticData ForLevel(string sceneKey);

    HeroStaticData ForHero();
    WindowConfig ForWindow(WindowId window);
    SoundStaticData ForSounds();
  }
}