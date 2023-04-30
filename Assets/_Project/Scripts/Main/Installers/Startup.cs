using _Project.Scripts.Main.AppServices;
using _Project.Scripts.Main.Game;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Main.Installers
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private ScreenServiceInstaller _screenServiceInstaller;

        public void Awake() 
        {
            Debug.Log("Startup");

            AppContext.Instantiate();
            DOTween.SetTweensCapacity(1000, 50);
            Services.Register<ScreenService>(_screenServiceInstaller);
            
            var t = Services.Get<ScreenService>();
        }
    }
}