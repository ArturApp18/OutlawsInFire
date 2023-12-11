using Codebase.Hero;
using Codebase.Infrastructure.Services.Factory;
using Codebase.Infrastructure.Services.PersistentProgress;
using Codebase.Logic;
using Codebase.Logic.EnemySpawners;
using Codebase.UI.Services.Factory;

namespace Codebase.Infrastructure.States
{
	public class GameLoopState : IState
	{
		private readonly IGameStateMachine _gameStateMachine;

		private readonly IPersistentProgressService _progressService;
		private readonly LoadingCurtain _loadingCurtain;

		private readonly IGameFactory _gameFactory;
		private readonly IUIFactory _uiFactory;

		private readonly SceneLoader _sceneLoader;
		private float _monstersForWave;

		public GameLoopState(IGameStateMachine gameStateMachine, IPersistentProgressService progressService, LoadingCurtain loadingCurtain, IGameFactory gameFactory,
			IUIFactory uiFactory)
		{
			_gameStateMachine = gameStateMachine;

			_progressService = progressService;
			_loadingCurtain = loadingCurtain;

			_gameFactory = gameFactory;
			_uiFactory = uiFactory;
		}

		public void Exit()
		{
			DescribeHeroDeath();
			StopWave();

			_progressService.Progress.KillData.ResetKillData();
		}

		public void Enter()
		{
			SubscribeHeroDeath();

			StartWave();
		}

		public void Update() { }

		private void SubscribeHeroDeath()
		{
			_gameFactory.HeroGameObject.TryGetComponent(out HeroDeath heroDeath);
			heroDeath.Restart += Lose;
		}

		private void DescribeHeroDeath()
		{
			_gameFactory.HeroGameObject.TryGetComponent(out HeroDeath heroDeath);
			heroDeath.Restart -= Lose;
		}


		private void StartWave()
		{
			foreach (SpawnPoint spawner in _gameFactory.Spawners)
			{
				spawner.StartSpawnNormalMob();
			}
		}

		private void StopWave()
		{
			foreach (SpawnPoint spawner in _gameFactory.Spawners)
			{
				spawner.StopSpawn();
			}
		}

		private void Lose() =>
			_uiFactory.CreateScoreMenu();
	}

}