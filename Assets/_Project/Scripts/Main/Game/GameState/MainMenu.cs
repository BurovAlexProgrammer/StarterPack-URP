using _Project.Scripts.Main.AppServices;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Main.Game.GameState
{
    public static partial class GameStates
    {
        public class MainMenu : GameState
        {
            public override async UniTask EnterState()
            {
                Old_Services.AudioService.PlayMusic(AudioService.MusicPlayerState.MainMenu).Forget();
                await Old_Services.SceneLoaderService.LoadSceneAsync(Old_SceneLoaderService.Scenes.MainMenu);
            }
        }
    }
}