using Codebase.Infrastructure.Services.PersistentProgress;
using Codebase.Infrastructure.Services.SaveLoad;
using Codebase.Infrastructure.States;
using Codebase.UI.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.UI.Menu
{
	public class MainMenu : WindowBase
	{
		[SerializeField] private Button _playButton;
		[SerializeField] private Button _quitButton;
		
		public void Construct(IPersistentProgressService progressService,IGameStateMachine gameStateMachine)
		{
			ProgressService = progressService;
			GameStateMachine = gameStateMachine;
		}

		protected override void SubscribeUpdates()
		{
			base.SubscribeUpdates();
			_playButton.onClick.AddListener(LoadLevelState);
			_quitButton.onClick.AddListener(Application.Quit);

		}

		private void LoadLevelState() =>
			GameStateMachine.Enter<LoadLevelState, string>(ProgressService.Progress.WorldData.PositionOnLevel.Level);

	}
}