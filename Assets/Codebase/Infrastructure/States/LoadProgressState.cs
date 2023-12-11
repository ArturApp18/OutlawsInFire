using Codebase.Data;
using Codebase.Infrastructure.Services.PersistentProgress;
using Codebase.Infrastructure.Services.SaveLoad;
using Codebase.Infrastructure.Services.StaticData;
using Codebase.StaticData;

namespace Codebase.Infrastructure.States
{
  public class LoadProgressState : IState
  {
    private const string InitialLevel = "Main";
    private readonly GameStateMachine _gameStateMachine;
    private readonly IPersistentProgressService _progressService;
    private readonly ISaveLoadService _saveLoadProgress;
    private readonly IStaticDataService _staticDataService;

    public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadProgress, IStaticDataService staticDataService)
    {
      _gameStateMachine = gameStateMachine;
      _progressService = progressService;
      _saveLoadProgress = saveLoadProgress;
      _staticDataService = staticDataService;
    }

    public void Enter()
    {
      LoadProgressOrInitNew();

      _gameStateMachine.Enter<MainMenuState>();
    }

    public void Exit()
    {
    }

    public void Update()
    {
      
    }

    private void LoadProgressOrInitNew()
    {
      _progressService.Progress = 
        _saveLoadProgress.LoadProgress() 
        ?? NewProgress();
    }

    private PlayerProgress NewProgress()
    {
      PlayerProgress progress =  new PlayerProgress(InitialLevel);

      HeroStaticData heroData = _staticDataService.ForHero();

      progress.HeroStats.MaxHP = heroData.Hp;
      progress.HeroStats.Damage = heroData.Damage;
      progress.HeroStats.AttackRadius = heroData.Cleavage;
      
      progress.HeroStats.ResetHp();
    
      return progress;
    }
  }
}