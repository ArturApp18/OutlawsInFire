using Codebase.Infrastructure.Services.PersistentProgress;
using Codebase.Infrastructure.Services.SaveLoad;
using Codebase.Infrastructure.States;
using Codebase.UI.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.UI.Menu
{
	public class ScoreMenu : WindowBase
	{
		[SerializeField] private Button _mainMenuButton;
		[SerializeField] private Button _restartButton;

		public void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService, IGameStateMachine gameStateMachine)
		{
			ProgressService = progressService;
			SaveLoadService = saveLoadService;
			GameStateMachine = gameStateMachine;
		}

		protected override void SubscribeUpdates()
		{
			base.SubscribeUpdates();
			_mainMenuButton.onClick.AddListener(GoToMainMenu);
			_restartButton.onClick.AddListener(Restart);
		}

		private void Restart() =>
			GameStateMachine.Enter<RestartLevelState>();

		private void GoToMainMenu() =>
			GameStateMachine.Enter<MainMenuState>();

	}

}