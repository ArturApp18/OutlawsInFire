using Codebase.Infrastructure.AssetManagement;
using Codebase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Audio;
using Codebase.Infrastructure.Services.Factory;
using Codebase.Infrastructure.Services.Inputs;
using Codebase.Infrastructure.Services.PersistentProgress;
using Codebase.Infrastructure.Services.Randomizer;
using Codebase.Infrastructure.Services.SaveLoad;
using Codebase.Infrastructure.Services.StaticData;
using Codebase.UI.Services.Factory;
using Codebase.UI.Services.Windows;
using UnityEngine;

namespace Codebase.Infrastructure.States
{
	public class BootstrapState : IState
	{
		private const string Initial = "Initial";
		private readonly IGameStateMachine _gameStateMachine;
		private readonly SceneLoader _sceneLoader;
		private readonly AllServices _services;
		private readonly AudioSource _audioSource;

		public BootstrapState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services, AudioSource audioSource)
		{
			_gameStateMachine = gameStateMachine;
			_sceneLoader = sceneLoader;
			_services = services;
			_audioSource = audioSource;
			RegisterServices();
		}

		public void Enter() =>
			_sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);

		public void Exit() { }
		public void Update() { }


		private void RegisterServices()
		{
			RegisterStaticDataService();

			_services.RegisterSingle(_gameStateMachine);
			_services.RegisterSingle<IAssetProvider>(new AssetProvider());
			_services.RegisterSingle<IInputService>(new InputService());
			_services.RegisterSingle<IRandomService>(new RandomService());
			_services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
			_services.RegisterSingle<IAudioService>(new AudioService(_services.Single<IStaticDataService>(), _audioSource, _services.Single<IRandomService>()));

			_services.RegisterSingle<IGameFactory>(new GameFactory(
				_services.Single<IAssetProvider>(),
				_services.Single<IStaticDataService>(),
				_services.Single<IRandomService>(),
				_services.Single<IInputService>(),
				_services.Single<IPersistentProgressService>(),
				_services.Single<IAudioService>()));

			_services.RegisterSingle<ISaveLoadService>(new SaveLoadService(
				_services.Single<IPersistentProgressService>(),
				_services.Single<IGameFactory>()));

			_services.RegisterSingle<IUIFactory>(new UIFactory(
				_services.Single<IAssetProvider>(),
				_services.Single<IStaticDataService>(),
				_services.Single<IPersistentProgressService>(),
				_services.Single<IGameFactory>(),
				_services.Single<ISaveLoadService>(),
				_gameStateMachine));

			_services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IUIFactory>()));
		}

		private void RegisterStaticDataService()
		{
			IStaticDataService staticData = new StaticDataService();
			staticData.Load();
			_services.RegisterSingle(staticData);
		}

		private void EnterLoadLevel() =>
			_gameStateMachine.Enter<LoadProgressState>();
	}
}