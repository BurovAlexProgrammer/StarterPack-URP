using System;
using _Project.Scripts.Extension;
using _Project.Scripts.Main.DTO.Enums;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Main.AppServices
{
    public class SceneLoaderService : IService, IConstruct
    {
        private Scene _currentScene;
        private Scene _preparedScene;
        private Scene _initialScene;

        public void Construct()
        {
            _initialScene = SceneManager.GetActiveScene();
        }
        
        public async UniTask LoadSceneAsync(SceneName sceneName)
        {
           // var sceneName = GetSceneName(scene);
            await UniTask.WhenAll(PrepareScene(sceneName));
            ActivatePreparedScene();
        }

        public async void ReloadActiveScene()
        {
            await SceneManager.UnloadSceneAsync(_currentScene);
            var asyncOperationHandle = Addressables.LoadSceneAsync(_currentScene.name, LoadSceneMode.Additive);
            await asyncOperationHandle.Task;
            var sceneInstance = asyncOperationHandle.Result;
            _preparedScene = sceneInstance.Scene;
            _preparedScene.SetActive(false);
        }
        
        public async void UnloadActiveScene()
        {
            await SceneManager.UnloadSceneAsync(_currentScene);
        }

        private async UniTask PrepareScene(SceneName sceneName)
        {
            _currentScene = SceneManager.GetActiveScene();
            var asyncOperationHandle = Addressables.LoadSceneAsync(Enum.GetName(sceneName.GetType(), sceneName) , LoadSceneMode.Additive);
            await asyncOperationHandle.Task;
            var sceneInstance = asyncOperationHandle.Result;
            _preparedScene = sceneInstance.Scene;
            _preparedScene.SetActive(false);
        }

        private void ActivatePreparedScene()
        {
            _preparedScene.SetActive(true);
            SceneManager.SetActiveScene(_preparedScene);
            SceneManager.UnloadSceneAsync(_currentScene);
        }
    }
}