using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.States;
using Codebase.Logic;
using UnityEngine;

namespace Codebase.Infrastructure
{
  public class Game
  {
    public readonly GameStateMachine GameStateMachine;

    public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain, AudioSource audioSource)
    {
      GameStateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container, audioSource);
    }
  }
}