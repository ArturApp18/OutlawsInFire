using System;

namespace Codebase.Data
{
  [Serializable]
  public class PlayerProgress
  {
    public WorldData WorldData;
    public Stats HeroStats;
    public KillData KillData;
    public ProgressMultiplierData ProgressMultiplierData;


    public PlayerProgress(string initialLevel)
    {
      WorldData = new WorldData(initialLevel);
      HeroStats = new Stats();
      KillData = new KillData();
      ProgressMultiplierData = new ProgressMultiplierData();
    }
  }

}