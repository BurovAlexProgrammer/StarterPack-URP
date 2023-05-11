using _Project.Scripts.Main.AppServices;
using _Project.Scripts.Main.DTO.Enums;
using _Project.Scripts.Main.Events;
using _Project.Scripts.Main.Wrappers;

namespace _Project.Scripts.Main.Systems
{
    public class SceneLoaderSystem : BaseSystem
    {
        private SceneLoaderService _sceneLoader;

        public override void Init()
        {
            base.Init();
            _sceneLoader = Services.Get<SceneLoaderService>();
        }

        public override void AddEventHandlers()
        {
            base.AddEventHandlers();
            AddListener<StartupSystemsInitializedEvent>(StartupSystemsInitialized);
            AddListener<ShowMainMenuEvent>(ShowMainMenu);
        }

        private void ShowMainMenu(BaseEvent obj)
        {
            _sceneLoader.LoadSceneAsync(SceneName.MainMenu);
        }

        private void StartupSystemsInitialized(BaseEvent evnt)
        {
            Log.Info("Initialized");
            _sceneLoader.LoadSceneAsync(SceneName.Intro);
        }

        public override void RemoveEventHandlers()
        {
            base.RemoveEventHandlers();
        }

        public override void OnDispose()
        {
            base.OnDispose();
        }
    }
}