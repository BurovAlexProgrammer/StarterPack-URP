﻿using _Project.Scripts.Extension;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Main.Game.GameStates
{
    public static partial class GameState
    {
        public class RestartGame : GameStateBase
        {
            public override async UniTask EnterState()
            {
                var currentScene = SceneManager.GetActiveScene();
                var newScene = SceneManager.CreateScene("Empty");
                newScene.SetActive(true);
            
                await SceneManager.UnloadSceneAsync(currentScene);
            }
        }
    }
}