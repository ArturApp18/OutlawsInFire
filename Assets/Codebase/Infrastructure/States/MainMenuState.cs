using Codebase.Infrastructure.Services.PersistentProgress;
using Codebase.Infrastructure.Services.StaticData;
using Codebase.Logic;
using Codebase.StaticData;
using Codebase.UI.Menu;
using Codebase.UI.Services.Factory;
using UnityEngine.SceneManagement;

namespace Codebase.Infrastructure.States
{
	public class MainMenuState : IState
	{
		private const string MainMenu = "MainMenu";
		private readonly SceneLoader _sceneLoader;
		private readonly LoadingCurtain _loadingCurtain;
		private readonly IUIFactory _uiFactory;
		private readonly IStaticDataService _dataService;
		private readonly IPersistentProgressService _progressService;
		private readonly IGameStateMachine _stateMachine;

		public MainMenuState(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IUIFactory uiFactory, IStaticDataService dataService,
			IPersistentProgressService progressService, IGameStateMachine stateMachine)
		{
			_sceneLoader = sceneLoader;
			_loadingCurtain = loadingCurtain;
			_uiFactory = uiFactory;
			_dataService = dataService;
			_progressService = progressService;
			_stateMachine = stateMachine;
		}

		public void Exit() { }

		public void Update() { }

		public void Enter()
		{
			_loadingCurtain.Show();

			_sceneLoader.Load(MainMenu, OnLoaded);
		}

		private void OnLoaded()
		{
			InitUIRoot();
			InitMainMenu();
			_loadingCurtain.Hide();
		}

		private void InitMainMenu()
		{
			MainMenu mainMenu = _uiFactory.CreateMainMenu();
			mainMenu.Construct(_progressService, _stateMachine);
		}
		
		private void InitUIRoot() =>
			_uiFactory.CreateUIRoot();
	}
}