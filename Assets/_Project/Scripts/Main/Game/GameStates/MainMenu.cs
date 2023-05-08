using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Main.Game.GameStates
{
    public static partial class GameState
    {
        public class MainMenu : GameStateBase
        {
            public override async UniTask EnterState()
            {
                // Old_Services.AudioService.PlayMusic(AudioService.MusicPlayerState.MainMenu).Forget();
                // await Old_Services.SceneLoaderService.LoadSceneAsync(Old_SceneLoaderService.Scenes.MainMenu);
            }
        }
    }
}