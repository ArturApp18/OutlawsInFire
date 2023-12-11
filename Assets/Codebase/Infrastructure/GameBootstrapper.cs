using Codebase.Infrastructure.States;
using Codebase.Logic;
using UnityEngine;

namespace Codebase.Infrastructure
{
  public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
  {
    public AudioSource AudioSource;
    public LoadingCurtain CurtainPrefab;
    
    private Game _game;

    private void Awake()
    {
      _game = new Game(this, Instantiate(CurtainPrefab), AudioSource);
      _game.GameStateMachine.Enter<BootstrapState>();

      DontDestroyOnLoad(this);
    }

    private void Update() =>
      _game.GameStateMachine.Update();
  }
}