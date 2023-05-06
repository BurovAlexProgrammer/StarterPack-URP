using _Project.Scripts.Main.AppServices;
using _Project.Scripts.Main.DTO.Enums;
using _Project.Scripts.Main.Events;
using _Project.Scripts.Main.Wrappers;

namespace _Project.Scripts.Main.Systems
{
    public class SceneLoaderSystem : BaseSystem
    {
        public override void Init()
        {
            base.Init();
        }

        public override void AddEventHandlers()
        {
            base.AddEventHandlers();
            AddListener<StartupSystemsInitializedEvent>(StartupSystemsInitialized);
        }

        private void StartupSystemsInitialized(BaseEvent evnt)
        {
            Log.Info("Initialized");
            Services.Get<SceneLoaderService>().LoadSceneAsync(SceneName.Intro);
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