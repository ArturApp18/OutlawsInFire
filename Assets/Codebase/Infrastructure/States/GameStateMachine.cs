using System;
using System.Collections.Generic;
using Codebase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Audio;
using Codebase.Infrastructure.Services.Factory;
using Codebase.Infrastructure.Services.PersistentProgress;
using Codebase.Infrastructure.Services.SaveLoad;
using Codebase.Infrastructure.Services.StaticData;
using Codebase.Logic;
using Codebase.UI.Services.Factory;
using UnityEngine;

namespace Codebase.Infrastructure.States
{
	public class GameStateMachine : IGameStateMachine
	{
		private readonly AllServices _services;
		private Dictionary<Type, IExitableState> _states;
		private IExitableState _activeState;

		public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, AllServices services, AudioSource audioSource)
		{
			_services = services;

			_states = new Dictionary<Type, IExitableState> {
				[typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services, audioSource),
				[typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingCurtain, services.Single<IGameFactory>(),
					services.Single<IPersistentProgressService>(), services.Single<IStaticDataService>(), services.Single<IUIFactory>()),
				[typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IPersistentProgressService>(), services.Single<ISaveLoadService>(), services.Single<IStaticDataService>()),
				[typeof(MainMenuState)] = new MainMenuState(sceneLoader, loadingCurtain,services.Single<IUIFactory>(), services.Single<IStaticDataService>(), services.Single<IPersistentProgressService>(), this),
				[typeof(GameLoopState)] = new GameLoopState(this, services.Single<IPersistentProgressService>(), loadingCurtain, services.Single<IGameFactory>(), services.Single<IUIFactory>()),
				[typeof(RestartLevelState)] = new RestartLevelState(services.Single<IGameFactory>(), loadingCurtain, this, sceneLoader),
			};
		}

		public void Enter<TState>() where TState : class, IState
		{
			IState state = ChangeState<TState>();
			state.Enter();
		}

		public void Update()
		{
			_activeState?.Update();
			_services?.Single<IAudioService>().Update();
		}

		public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
		{
			TState state = ChangeState<TState>();
			state.Enter(payload);
		}

		private TState ChangeState<TState>() where TState : class, IExitableState
		{
			_activeState?.Exit();

			TState state = GetState<TState>();
			_activeState = state;

			return state;
		}

		private TState GetState<TState>() where TState : class, IExitableState =>
			_states[typeof(TState)] as TState;
	}
}