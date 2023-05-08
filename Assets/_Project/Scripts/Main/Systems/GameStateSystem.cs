using _Project.Scripts.Main.AppServices;
using _Project.Scripts.Main.Events;
using GameState = _Project.Scripts.Main.Game.GameStates.GameState;

namespace _Project.Scripts.Main.Systems
{
    public class GameStateSystem : BaseSystem
    {
        private GameStateService _gameStateService;
        public override void Init()
        {
            base.Init();
            _gameStateService = Services.Get<GameStateService>();
        }

        public override void RemoveEventHandlers()
        {
            base.RemoveEventHandlers();
            RemoveListener<StartupSystemsInitializedEvent>();
            RemoveListener<IntroEndEvent>();
        }
        
        public override void AddEventHandlers()
        {
            base.AddEventHandlers();
            AddListener<StartupSystemsInitializedEvent>(StartupSystemsInitialized);
            AddListener<IntroEndEvent>(IntroEnded);
        }

        private void IntroEnded(BaseEvent baseEvent)
        {
            _gameStateService.SetState<GameState.MainMenu>();
        }

        private void StartupSystemsInitialized(BaseEvent baseEvent)
        {
            _gameStateService.SetState<GameState.Intro>();
        }
    }
}