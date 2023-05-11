using System;
using System.Linq;
using _Project.Scripts.Main.DTO.Enums;
using _Project.Scripts.Main.Events;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Main.AppServices
{
    public class GameStateService : IService, IConstruct
    {
        private bool _isGamePause;
        private bool _transaction;
        private bool _isGameOver;
        private bool _isMenuMode;
        
        private GameState _currentState;
        
        private StatisticService _statisticService;
        private SceneLoaderService _sceneLoader;

        public event Action<bool> SwitchPause;
        public event Action StateChanged;
        public event Action OnGameOver;
        
        public GameState CurrentState => _currentState;
        public bool IsGamePause => _isGamePause;
        public bool IsGameOver => _isGameOver;
        public bool IsTransaction => _transaction;
        public bool IsMenuMode => _isMenuMode;


        public async void Construct()
        {
            _statisticService = Services.Get<StatisticService>();
            _sceneLoader = Services.Get<SceneLoaderService>();

            if (_sceneLoader.IsCustomScene())
            {
                await _sceneLoader.LoadSceneAsync(SceneName.Boot);
                SetState(GameState.CustomScene);
            }
        }

        public void SetPause(bool value)
        {
            _isGamePause = value;
            SwitchPause?.Invoke(_isGamePause);
        }
        

        public void SetState(GameState newState)
        {
            if (_currentState == newState)
            {
                Debug.Log($"GameState: {newState.ToString()} (Already entered, skipped)");
                return;
            }
            
            _currentState = newState;

            Debug.Log($"GameState: <color=#39A5E6> {newState.ToString()}</color>");
            StateChanged?.Invoke();
        }
        
        public void RestartGame()
        {
            _isGameOver = false;
            new RestartGameEvent().Fire();
        }

        public void QuitGame()
        {
            new QuitGameEvent().Fire();
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
        
        public bool CurrentStateIs(params GameState[] states)
        {
            return states.Any(x => x == CurrentState);
        }
        
        public bool CurrentStateIsNot(params GameState[] states)
        {
            return states.All(x => x != CurrentState);
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