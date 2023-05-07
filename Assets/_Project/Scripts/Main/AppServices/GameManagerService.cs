using System;
using _Project.Scripts.Extension;
using _Project.Scripts.Extension.Attributes;
using _Project.Scripts.Main.Game;
using _Project.Scripts.Main.Game.GameState;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace _Project.Scripts.Main.AppServices
{
    public class GameManagerService : IService, IConstruct
    {
        [SerializeField, ReadOnlyField] private GameStateMachine _gameStateMachine;
        [SerializeField, ReadOnlyField] private bool _isGamePause;

        private ControlService _controlService;
        private StatisticService _statisticService;

        public event Action<bool> SwitchPause;
        public event Action GameOver;

        private bool _transaction;
        private bool _isGameOver;

        public GameState ActiveGameState => _gameStateMachine.ActiveState;
        public bool IsGamePause => _isGamePause;
        public bool IsGameOver => _isGameOver;
        
        public void Construct()
        {
            _controlService = Services.Get<ControlService>();
            _statisticService = Services.Get<StatisticService>();

            _controlService.Controls.Player.Pause.BindAction(BindActions.Started, PauseGame);
            _gameStateMachine.Init().Forget();
        }

        public async UniTask SetGameState(GameState newState)
        {
            await _gameStateMachine.SetState(newState);
        }

        public void RestartGame()
        {
            _isGameOver = false;
            RestoreTimeSpeed();
            _statisticService.EndGameDataSaving();
            _gameStateMachine.SetState(new GameStates.RestartGame()).Forget();
            _gameStateMachine.SetState(new GameStates.PlayNewGame()).Forget();
        }

        public void QuitGame()
        {
            _gameStateMachine.SetState(new GameStates.QuitGame()).Forget();
        }

        public void GoToMainMenu()
        {
            _statisticService.EndGameDataSaving();
            _gameStateMachine.SetState(new GameStates.MainMenu()).Forget();
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

            if (ActiveStateEquals<GameStates.PlayNewGame>() == false &&
                ActiveStateEquals<GameStates.CustomScene>() == false) return;

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

            if (ActiveStateEquals<GameStates.PlayNewGame>() == false &&
                ActiveStateEquals<GameStates.CustomScene>() == false) return;

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

        public bool ActiveStateEquals<T>() where T : GameState
        {
            return ActiveGameState.EqualsState(typeof(T));
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