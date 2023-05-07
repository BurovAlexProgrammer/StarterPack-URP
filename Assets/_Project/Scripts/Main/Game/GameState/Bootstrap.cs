using _Project.Scripts.Extension;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Main.Game.GameState
{
    public static partial class GameStates
    {
        public class Bootstrap : GameState
        {
            public override async UniTask EnterState()
            {
                //Services.Get<SceneLoaderService>().ShowScene();
                await 3f.WaitInSeconds();
            }
        }
    }
}