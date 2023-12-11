using System.Collections.Generic;
using System.Linq;
using Codebase.StaticData;
using Codebase.StaticData.Windows;
using Codebase.UI.Services.Windows;
using UnityEngine;

namespace Codebase.Infrastructure.Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private const string MonstersDataPath = "StaticData/Enemies";
    private const string LevelsDataPath = "StaticData/Levels";
    private const string HeroDataPath = "StaticData/Hero/Hero";
    private const string SoundsDataPath = "StaticData/Sound/Sound";
    private const string StaticDataWindowPath = "StaticData/UI/Windows";


    private Dictionary<EnemyTypeId, EnemyStaticData> _monsters;
    private Dictionary<string, LevelStaticData> _levels;
    private Dictionary<WindowId, WindowConfig> _windowConfigs;
    private HeroStaticData _hero;
    private SoundStaticData _sounds;

    public void Load()
    {
      _hero = Resources.Load<HeroStaticData>(HeroDataPath);

      _sounds = Resources.Load<SoundStaticData>(SoundsDataPath);
      
      _monsters = Resources
        .LoadAll<EnemyStaticData>(MonstersDataPath)
        .ToDictionary(x => x.EnemyTypeId, x => x);

      _levels = Resources
        .LoadAll<LevelStaticData>(LevelsDataPath)
        .ToDictionary(x => x.LevelKey, x => x);

      _windowConfigs = Resources
        .Load<WindowStaticData>(StaticDataWindowPath)
        .Configs
        .ToDictionary(x => x.WindowId, x => x);
    }

    public SoundStaticData ForSounds() =>
      _sounds;
    
    public EnemyStaticData ForMonster(EnemyTypeId typeId) =>
      _monsters.TryGetValue(typeId, out EnemyStaticData staticData)
        ? staticData
        : null;

    public LevelStaticData ForLevel(string sceneKey) =>
      _levels.TryGetValue(sceneKey, out LevelStaticData staticData)
        ? staticData
        : null;

    public WindowConfig ForWindow(WindowId window) =>
      _windowConfigs.TryGetValue(window, out WindowConfig windowConfig)
        ? windowConfig
        : null;

    public HeroStaticData ForHero() =>
      _hero;

  }

 
}