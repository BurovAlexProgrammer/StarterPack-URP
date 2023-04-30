using _Project.Scripts.Main.AppServices;
using _Project.Scripts.Main.Game;
using _Project.Scripts.Main.Systems;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Main.Installers
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private ScreenServiceInstaller _screenServiceInstaller;
        [SerializeField] private ControlServiceInstaller _controlServiceInstaller;

        public void Awake() 
        {
            Debug.Log("Startup");

            AppContext.Instantiate();
            DOTween.SetTweensCapacity(1000, 50);
            Services.Register<ControlService>(_controlServiceInstaller);
            Services.Register<ScreenService>(_screenServiceInstaller);
            
            var t = Services.Get<ScreenService>();
            
            SystemsService.Bind<ControlSystem>();
            SystemsService.Bind<ScreenSystem>();
        }
    }
}