using _Project.Scripts.Main.Events;
using _Project.Scripts.Main.Services;

namespace _Project.Scripts.Main.Systems
{
    public class StatisticSystem : BaseSystem
    {
        private StatisticService _statisticService;
        
        public override void RemoveEventHandlers()
        {
            RemoveListener<GoToMainMenuEvent>();
            RemoveListener<RestartGameEvent>();
            RemoveListener<PlayGameEvent>();
            base.RemoveEventHandlers();
        }
        public override void AddEventHandlers()
        {
            base.AddEventHandlers();
            AddListener<GoToMainMenuEvent>(OnGoToMainMenu);
            AddListener<RestartGameEvent>(OnRestartGame);
            AddListener<PlayGameEvent>(OnPlayGame);
        }

        private void OnPlayGame(BaseEvent obj)
        {
            _statisticService.ResetSessionRecords();
        }

        private void OnRestartGame(BaseEvent obj)
        {
            _statisticService.EndGameDataSaving();
        }

        private void OnGoToMainMenu(BaseEvent obj)
        {
            _statisticService.EndGameDataSaving();
        }
    }
}