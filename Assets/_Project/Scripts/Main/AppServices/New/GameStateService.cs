using System;
using _Project.Scripts.Extension;
using _Project.Scripts.Main.Game.GameStates;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using GameState = _Project.Scripts.Main.Game.GameStates.GameState;

namespace _Project.Scripts.Main.AppServices
{
    public class GameStateService : IService, IConstruct
    {
        private bool _isGamePause;
        private bool _transaction;
        private bool _isGameOver;
        private GameStateBase _currentState;

        private ControlService _controlService;
        private StatisticService _statisticService;

        public event Action<bool> SwitchPause;
        public event Action StateChanged;
        public event Action GameOver;
        
        public GameStateBase CurrentState => _currentState;
        public bool IsGamePause => _isGamePause;
        public bool IsGameOver => _isGameOver;



        
        public async void Construct()
        {
            _controlService = Services.Get<ControlService>();
            _statisticService = Services.Get<StatisticService>();

            _controlService.Controls.Player.Pause.BindAction(BindActions.Started, PauseGame);
            
            // if (_sceneLoader.InitialSceneEquals(SceneName.Boot))
            {
                await SetStateAsync<GameState.Boot>();
                //await SetState(new GameState.MainMenu());
                return;
            }
            
            await SetStateAsync<GameState.CustomScene>();
        }

        ~GameStateService()
        {
            _controlService.Controls.Player.Pause.UnbindAction(BindActions.Started, PauseGame);
        }

        public async UniTask SetStateAsync<T>() where T: GameStateBase
        {
            var newState = Activator.CreateInstance<T>();
            if (_currentState == newState)
            {
                Debug.Log($"GameState Enter: {newState.GetType().Name} (Already entered, skipped)");
                return;
            }

            if (_currentState != null)
            {
                Debug.Log("GameState Exit: " + _currentState.GetType().Name);
                await _currentState.ExitState();
            }

            _currentState = newState;

            Debug.Log($"GameState Enter: <color=#39A5E6> {newState.GetType().Name}</color>");
            await _currentState.EnterState();
            StateChanged?.Invoke();
        }
        
        public void SetState<T>() where T: GameStateBase
        {
            _ = SetStateAsync<T>();
        }

        public void RestartGame()
        {
            _isGameOver = false;
            RestoreTimeSpeed();
            _statisticService.EndGameDataSaving();
            SetState<GameState.RestartGame>();
            SetState<GameState.PlayNewGame>();
        }

        public void QuitGame()
        {
            SetState<GameState.QuitGame>();
        }

        public void GoToMainMenu()
        {
            _statisticService.EndGameDataSaving();
            //SetState(new GameState.MainMenu()).Forget();
        }

        public void PrepareToPlay()
        {
            // Old_Services.AudioService.PlayMusic(AudioService.MusicPlayerState.Battle).Forget();
            // Old_Services.ControlService.LockCursor();
            // Old_Services.ControlService.Controls.Player.Enable();
            // Old_Services.ControlService.Controls.Menu.Disable();
            // Old_Services.StatisticService.ResetSessionRecords();
        }

        public async void PauseGame(InputAction.CallbackContext ctx)
        {
            if (_transaction) return;

            if (ActiveStateEquals<GameState.PlayNewGame>() == false &&
                ActiveStateEquals<GameState.CustomScene>() == false) return;

            Debug.Log("Game paused to menu.");

            var fixedDeltaTime = Time.fixedDeltaTime;
            _transaction = true;
            _isGamePause = true;
            _controlService.Controls.Player.Disable();
            _controlService.UnlockCursor();
            SwitchPause?.Invoke(_isGamePause);

            await FluentSetTimeScale(0f);

            _controlService.Controls.Menu.Enable();
            Time.fixedDeltaTime = fixedDeltaTime;
            _transaction = false;
        }

        public async void ReturnGame()
        {
            if (_isGameOver) return;
            if (_transaction) return;

            if (ActiveStateEquals<GameState.PlayNewGame>() == false &&
                ActiveStateEquals<GameState.CustomScene>() == false) return;

            Debug.Log("Game returned from pause.");
            _transaction = true;
            _isGamePause = false;
            SwitchPause?.Invoke(_isGamePause);
            _controlService.Controls.Player.Enable();
            _controlService.LockCursor();

            await FluentSetTimeScale(1f);

            _controlService.Controls.Menu.Disable();
            _transaction = false;
        }

        public async void RunGameOver()
        {
            Debug.Log("Game Over");
            _statisticService.EndGameDataSaving();
            _controlService.Controls.Player.Disable();

            await FluentSetTimeScale(1f);

            _controlService.UnlockCursor();
            _controlService.Controls.Menu.Enable();

            _isGameOver = true;
            GameOver?.Invoke();
        }

        public bool ActiveStateEquals<T>() where T : GameStateBase
        {
            return CurrentState.EqualsState(typeof(T));
        }

        public void RestoreTimeSpeed()
        {
            SetTimeScale(1f);
        }

        private void AddScores(int value)
        {
            if (value < 0)
            {
                throw new Exception("Adding scores cannot be below zero.");
            }

            // _scores += value;
            // _statisticService.SetScores(_scores);
        }

        private void ReturnGame(InputAction.CallbackContext ctx)
        {
            ReturnGame();
        }

        private async UniTask FluentSetTimeScale(float scale)
        {
            var timeScale = Time.timeScale;
            await DOVirtual.Float(timeScale, scale, 1f, SetTimeScale)
                .SetUpdate(true)
                .AsyncWaitForCompletion();
        }

        private void SetTimeScale(float value)
        {
            Time.timeScale = value;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }
}