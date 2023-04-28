using _Project.Scripts.Main.AppServices;
using _Project.Scripts.Main.Game;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Main.Installers
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private IScreenService _screenService;

        public void Awake() 
        {
            Debug.Log("Startup");
            
            var gameContext = Instantiate(new GameObject());
            gameContext.name = "GameContext";
            gameContext.AddComponent<GameContext>();
            
            DOTween.SetTweensCapacity(1000, 50);
            Services.Register(_screenService);
            
            var t = Services.Get<IScreenService>();
        }
    }
}