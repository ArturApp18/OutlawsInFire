using Codebase.Infrastructure.Services.Factory;
using Codebase.Infrastructure.Services.PersistentProgress;
using Codebase.Infrastructure.Services.StaticData;
using Codebase.Logic;
using Codebase.StaticData;
using Codebase.UI.Elements;
using Codebase.UI.Services.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Codebase.Infrastructure.States
{
	public class LoadLevelState : IPayloadedState<string>
	{
		private const string InitialPointTag = "InitialPoint";

		private readonly GameStateMachine _gameStateMachine;
		private readonly SceneLoader _sceneLoader;
		private readonly LoadingCurtain _loadingCurtain;
		private readonly IGameFactory _gameFactory;
		private readonly IPersistentProgressService _progressService;
		private readonly IStaticDataService _staticData;
		private readonly IUIFactory _uiFactory;
		private GameObject _hero;
		private bool _firstEnter = true;

		public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory,
			IPersistentProgressService progressService, IStaticDataService staticDataService, IUIFactory uiFactory)
		{
			_gameStateMachine = gameStateMachine;
			_sceneLoader = sceneLoader;
			_loadingCurtain = loadingCurtain;
			_gameFactory = gameFactory;
			_progressService = progressService;
			_staticData = staticDataService;
			_uiFactory = uiFactory;
		}

		public void Enter(string sceneName)
		{
			_loadingCurtain.Show();
			_gameFactory.Cleanup();
			_sceneLoader.Load(sceneName, OnLoaded);
		}

		public void Exit()
		{
			_progressService.Progress.KillData.ResetKillData();
			_progressService.Progress.KillData.ResetWaveData();
			_loadingCurtain.Hide();
		}

		public void Update() { }

		private void OnLoaded()
		{
			InitUIRoot();
			InitGameWorld();
			InformProgressReaders();

			_gameStateMachine.Enter<GameLoopState>();
		}

		private void InitUIRoot() =>
			_uiFactory.CreateUIRoot();

		private void InformProgressReaders()
		{
			foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
				progressReader.LoadProgress(_progressService.Progress);
		}

		private void InitGameWorld()
		{
			LevelStaticData levelData = LevelStaticData();
			_hero = _gameFactory.CreateHero(levelData.InitialHeroPosition);
			
			InitSpawners(levelData);

			InitHud(_hero);
		}
		
		

		private void InitSpawners(LevelStaticData levelData)
		{
			foreach (EnemySpawnerStaticData spawnerData in levelData.EnemySpawners)
				_gameFactory.CreateSpawner(spawnerData.Id, spawnerData.Position, spawnerData.NormalEnemies, spawnerData.StrongEnemies);
		}

		private LevelStaticData LevelStaticData()
		{
			string sceneKey = SceneManager.GetActiveScene().name;
			LevelStaticData levelData = _staticData.ForLevel(sceneKey);
			return levelData;
		}

		private void InitHud(GameObject hero)
		{
			Hud hud = _gameFactory.CreateHud();
			hud.GetComponent<ActorUI>().Construct(hero.GetComponent<IHealth>());
		}
	}
}