using Codebase.Data;
using Codebase.Infrastructure.Services.Factory;
using Codebase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Codebase.Infrastructure.Services.SaveLoad
{
  public class SaveLoadService : ISaveLoadService
  {
    private const string ProgressKey = "Progress";
    
    private readonly IPersistentProgressService _progressService;
    private readonly IGameFactory _gameFactory;

    public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
    {
      _progressService = progressService;
      _gameFactory = gameFactory;
    }

    public void SaveProgress()
    {
      foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
        progressWriter.UpdateProgress(_progressService.Progress);
      
      PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
    }

    public PlayerProgress LoadProgress()
    {
      return PlayerPrefs.GetString(ProgressKey)?
        .ToDeserialized<PlayerProgress>();
    }
  }
}