using _Project.Scripts.Extension;
using _Project.Scripts.Main.DTO.Enums;
using _Project.Scripts.Main.Events;
using _Project.Scripts.Main.Services;
using _Project.Scripts.Main.Wrappers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Main.Systems
{
    public class GameStateSystem : BaseSystem
    {
        private GameStateService _gameStateService;
        private ControlService _controlService;
        private bool _transaction;
        
        public override void Init()
        {
            base.Init();
            _gameStateService = Services.Services.Get<GameStateService>();
            _controlService = Services.Services.Get<ControlService>();
            _controlService.Controls.Player.Pause.BindAction(BindActions.Started, PauseGame);
        }

        public override void OnDispose()
        {
            base.OnDispose();
            Services.Services.Get<ControlService>().Controls.Player.Pause.UnbindAction(BindActions.Started, PauseGame);
        }

        public override void RemoveEventHandlers()
        {
            base.RemoveEventHandlers();
            RemoveListener<GameOverEvent>();
            RemoveListener<StartupSystemsInitializedEvent>();
            RemoveListener<IntroEndEvent>();
            RemoveListener<RestartGameEvent>();
            RemoveListener<ShowMainMenuEvent>();
            RemoveListener<GoToMainMenuEvent>();
            RemoveListener<QuitGameEvent>();
        }
        
        public override void AddEventHandlers()
        {
            base.AddEventHandlers();
            AddListener<GameOverEvent>(OnGameOver);
            AddListener<StartupSystemsInitializedEvent>(StartupSystemsInitialized);
            AddListener<IntroEndEvent>(IntroEnded);
            AddListener<RestartGameEvent>(OnGameRestart);
            AddListener<ShowMainMenuEvent>(GoToMainMenu);
            AddListener<GoToMainMenuEvent>(GoToMainMenu);
            AddListener<QuitGameEvent>(OnQuitGame);
        }

        private void OnQuitGame(BaseEvent obj)
        {
            _gameStateService.QuitGame();
        }

        private void GoToMainMenu(BaseEvent obj)
        {
            _gameStateService.RestoreTimeSpeed();
            _gameStateService.SetState(GameState.MainMenu);
        }

        private void OnGameRestart(BaseEvent baseEvent)
        {
            _gameStateService.IsGameOver = false;
            _gameStateService.RestoreTimeSpeed();
        }

        private void IntroEnded(BaseEvent baseEvent)
        {
            new ShowMainMenuEvent().Fire();
        }

        private async void StartupSystemsInitialized(BaseEvent baseEvent)
        {
            _gameStateService.SetState(GameState.Intro);
            // await 3f.WaitInSeconds();
            await 0.3f.WaitInSeconds();
            new IntroEndEvent().Fire();
        }
        
        public async void PauseGame(InputAction.CallbackContext ctx)
        {
            if (_transaction) return;
            if (_gameStateService.CurrentStateIsNot(GameState.PlayGame, GameState.CustomScene)) return;

            Debug.Log("Game paused to menu.");

            _transaction = true;
            _gameStateService.SetPause(true);
            _controlService.Controls.Player.Disable();
            _controlService.SetMenuMode();

            await _gameStateService.FluentSetTimeScale(0f, 1f);

            _controlService.Controls.Menu.Enable();
            _transaction = false;
        }
        
        public async void ReturnGame()
        {
            if (_transaction) return;
            if (_gameStateService.IsGameOver) return;
            if (_gameStateService.CurrentStateIsNot(GameState.PlayGame, GameState.CustomScene)) return;

            Debug.Log("Game returned from pause.");
            
            _transaction = true;
            _gameStateService.SetPause(false);
            _controlService.Controls.Player.Enable();
            _controlService.SetPlayMode();

            await _gameStateService.FluentSetTimeScale(1f, 1f);

            _controlService.Controls.Menu.Disable();
            _transaction = false;
        }
        
        private async void OnGameOver(BaseEvent baseEvent)
        {
            Log.Info("Game Over");
            _controlService.Controls.Player.Disable();

            await _gameStateService.FluentSetTimeScale(1f, 1f);

            _controlService.SetMenuMode();
            _controlService.Controls.Menu.Enable();

            _gameStateService.GameOver();
        }
    }
}