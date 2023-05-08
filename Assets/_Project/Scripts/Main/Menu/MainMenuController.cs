using _Project.Scripts.Main.AppServices;
using _Project.Scripts.Main.Game.GameStates;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.Main.Menu
{
    public class MainMenuController : MenuController
    {
        // [Inject] private Old_SceneLoaderService _sceneLoader;
        [Inject] private GameStateService _gameManager;
        
        private void Start()
        {
            _ = EnterState(MenuStates.MainMenu);
        }


        public async void QuitGame()
        {
            // await _sceneLoader.HideScene();
            _gameManager.QuitGame();
        }

        public void StartNewGame()
        {
            _gameManager.SetState<GameState.PlayNewGame>();
        }

        protected override async UniTask EnterState(MenuStates newState)
        {
            await base.EnterState(newState);

            switch (newState)
            {
                case MenuStates.MainMenu:
                    break;
                case MenuStates.Settings:
                    break;
                case MenuStates.QuitGame:
                    break;
                case MenuStates.NewGame:
                    break;
            }
        }
        
        protected override async UniTask ExitState(MenuStates oldState)
        {
            await base.ExitState(oldState);
            
            switch (oldState)
            {
                case MenuStates.Settings:
                    break;
                case MenuStates.QuitGame:
                    break;
                case MenuStates.NewGame:
                    break;
            }
        }
    }
}