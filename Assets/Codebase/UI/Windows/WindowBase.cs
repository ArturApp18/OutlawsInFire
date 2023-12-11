using Codebase.Infrastructure.Services.PersistentProgress;
using Codebase.Infrastructure.Services.SaveLoad;
using Codebase.Infrastructure.States;
using UnityEngine;

namespace Codebase.UI.Windows
{
  public abstract class WindowBase : MonoBehaviour
  {
    protected IPersistentProgressService ProgressService;
    protected ISaveLoadService SaveLoadService;
    protected IGameStateMachine GameStateMachine;

    private void Awake() => 
      OnAwake();

    private void Start()
    {
      Initialize();
      SubscribeUpdates();
    }

    private void OnDestroy() => 
      Cleanup();

    protected virtual void OnAwake() { }
    protected virtual void Initialize(){}
    protected virtual void SubscribeUpdates(){}
    protected virtual void Cleanup(){}
  }
}