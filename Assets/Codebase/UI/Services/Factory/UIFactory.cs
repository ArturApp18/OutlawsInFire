using Codebase.Infrastructure.AssetManagement;
using Codebase.Infrastructure.Services.Factory;
using Codebase.Infrastructure.Services.PersistentProgress;
using Codebase.Infrastructure.Services.SaveLoad;
using Codebase.Infrastructure.Services.StaticData;
using Codebase.Infrastructure.States;
using Codebase.StaticData.Windows;
using Codebase.UI.Menu;
using Codebase.UI.Services.Windows;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Codebase.UI.Services.Factory
{
	public class UIFactory : IUIFactory
	{
		private const string UIRootPath = "UI/UIRoot";
		private readonly IAssetProvider _assets;
		private readonly IStaticDataService _staticData;
		private readonly IPersistentProgressService _progressService;
		private readonly IGameFactory _gameFactory;
		private readonly IGameStateMachine _gameStateMachine;
		private readonly ISaveLoadService _saveLoadService;

		private Transform _uiRoot;

		public UIFactory(IAssetProvider assets, IStaticDataService staticData, IPersistentProgressService progressService, IGameFactory gameFactory,
			ISaveLoadService saveLoadService, IGameStateMachine gameStateMachine)
		{
			_assets = assets;
			_staticData = staticData;
			_progressService = progressService;
			_gameFactory = gameFactory;
			_gameStateMachine = gameStateMachine;
			_saveLoadService = saveLoadService;
		}

		public ScoreMenu CreateScoreMenu()
		{
			WindowConfig config = _staticData.ForWindow(WindowId.ScoreMenu);
			ScoreMenu window = Object.Instantiate(config.Template, _uiRoot) as ScoreMenu;
			window.Construct(_progressService, _saveLoadService, _gameStateMachine);
			return window;
		}

		public void CreateUIRoot() =>
			_uiRoot = _assets.Instantiate(UIRootPath).transform;

		public MainMenu CreateMainMenu()
		{
			WindowConfig config = _staticData.ForWindow(WindowId.MainMenu);
			MainMenu window = Object.Instantiate(config.Template, _uiRoot) as MainMenu;
			window.Construct(_progressService, _gameStateMachine);
			return window;
		}
	}
}