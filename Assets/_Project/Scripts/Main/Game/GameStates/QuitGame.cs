using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Main.Game.GameStates
{
    public static partial class GameState
    {
        public class QuitGame : GameStateBase
        {
            public override async UniTask EnterState()
            {
                await UniTask.Yield();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
        }
    }
}