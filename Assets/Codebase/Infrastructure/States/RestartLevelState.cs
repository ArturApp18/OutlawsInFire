using Codebase.Infrastructure.Services.Factory;
using Codebase.Logic;

namespace Codebase.Infrastructure.States
{
	public class RestartLevelState : IState
	{
		private const string Initial = "Initial";
		
		private readonly IGameFactory _gameFactory;

		private readonly IGameStateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;

		public RestartLevelState(IGameFactory gameFactory, LoadingCurtain loadingCurtain, IGameStateMachine stateMachine, SceneLoader sceneLoader)
		{
			_gameFactory = gameFactory;
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
		}

		public void Enter()
		{
			_gameFactory.Dispose();

			_sceneLoader.Load(Initial, OnLoaded);
		}

		private void OnLoaded() =>
			_stateMachine.Enter<LoadProgressState>();

		public void Exit() { }

		public void Update() { }

	}
}