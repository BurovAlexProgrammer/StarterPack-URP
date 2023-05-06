using System;
using _Project.Scripts.Main.AppServices;
using _Project.Scripts.Main.Events;
using _Project.Scripts.Main.Systems;
using DG.Tweening;
using UnityEngine;
using AppContext = _Project.Scripts.Main.Game.AppContext;

namespace _Project.Scripts.Main.Installers
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private ScreenServiceInstaller _screenServiceInstaller;
        [SerializeField] private ControlServiceInstaller _controlServiceInstaller;
        [SerializeField] private DebugServiceInstaller _debugServiceInstaller;

        public void Awake() 
        {
            Debug.Log("Startup");

            AppContext.Instantiate();
            DOTween.SetTweensCapacity(1000, 50);
            Services.Register<ControlService>(_controlServiceInstaller);
            Services.Register<ScreenService>(_screenServiceInstaller);
            Services.Register<PoolService>();
            Services.Register<DebugService>(_debugServiceInstaller);
            
            SystemsService.Bind<ControlSystem>();
            SystemsService.Bind<ScreenSystem>();
            
            SystemsService.Bind<TestSystem>();
            
            new TestEvent(){Name = "Good"}.Fire();
            new Test2Event().Fire();
        }

        private void OnApplicationQuit()
        {
            Services.Dispose();
            SystemsService.Dispose();
        }
    }
}