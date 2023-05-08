using _Project.Scripts.Main.AppServices;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Main.Game.GameStates
{
    public static partial class GameState
    {
        public class CustomScene : GameStateBase
        {
            public override async UniTask EnterState()
            {
                await UniTask.Yield();
                Services.Get<GameStateService>().PrepareToPlay();
            }
        }
    }
}