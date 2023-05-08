using _Project.Scripts.Extension;
using _Project.Scripts.Main.Events;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Main.Game.GameStates
{
    public static partial class GameState
    {
        public class Intro : GameStateBase
        {
            public override async UniTask EnterState()
            {
                //Services.Get<SceneLoaderService>().ShowScene();
                await 3f.WaitInSeconds();
                new IntroEndEvent().Fire();
            }
        }
    }
}