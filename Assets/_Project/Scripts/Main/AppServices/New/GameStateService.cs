using System;
using System.Linq;
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
        private bool _isMenuMode;
        private GameStateBase _currentState;
        
        private StatisticService _statisticService;

        public event Action<bool> SwitchPause;
        public event Action StateChanged;
        public event Action OnGameOver;
        
        public GameStateBase CurrentState => _currentState;
        public bool IsGamePause => _isGamePause;
        public bool IsGameOver => _isGameOver;
        public bool IsTransaction => _transaction;
        public bool IsMenuMode => _isMenuMode;


        public async void Construct()
        {
            _statisticService = Services.Get<StatisticService>();

            // if (_sceneLoader.InitialSceneEquals(SceneName.Boot))
            {
                await SetStateAsync<GameState.Boot>();
                //await SetState(new GameState.MainMenu());
                return;
            }
            
            await SetStateAsync<GameState.CustomScene>();
        }

        public void SetPause(bool value)
        {
            _isGamePause = value;
            SwitchPause?.Invoke(_isGamePause);
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
            RestoreTimeSpeed();
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

        public void GameOver()
        {
            _isGameOver = true;
            OnGameOver?.Invoke();
        }

        public bool CurrentStateIs<T>() where T : GameStateBase
        {
            return CurrentState.EqualsState<T>();
        }
        
        public bool CurrentStateIsNot<T>() where T : GameStateBase
        {
            return !CurrentState.EqualsState<T>();
        }

        public bool CurrentStateIs(params Type[] states)
        {
            return states.Any(x => x == CurrentState.GetType());
        }
        
        public bool CurrentStateIsNot(params Type[] states)
        {
            return states.All(x => x != CurrentState.GetType());
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
        
        public async UniTask FluentSetTimeScale(float scale, float duration)
        {
            var fixedDeltaTime = Time.fixedDeltaTime;
            var timeScale = Time.timeScale;
            await DOVirtual.Float(timeScale, scale, duration, SetTimeScale)
                .SetUpdate(true)
                .AsyncWaitForCompletion();
            Time.fixedDeltaTime = fixedDeltaTime;
        }

        public void SetTimeScale(float value)
        {
            Time.timeScale = value;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }
}