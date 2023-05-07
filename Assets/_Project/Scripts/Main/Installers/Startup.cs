using System.Collections;
using _Project.Scripts.Main.AppServices;
using _Project.Scripts.Main.DTO;
using _Project.Scripts.Main.Events;
using _Project.Scripts.Main.Game;
using _Project.Scripts.Main.Systems;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Main.Installers
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private ScreenServiceInstaller _screenServiceInstaller;
        [SerializeField] private ControlServiceInstaller _controlServiceInstaller;
        [SerializeField] private DebugServiceInstaller _debugServiceInstaller;
        [SerializeField] private SettingsServiceInstaller _settingsServiceInstaller;
        [SerializeField] private AudioServiceInstaller _audioServiceInstaller;

        public void Awake() 
        {
            Debug.Log("Startup");

            AppContext.Instantiate();
            DOTween.SetTweensCapacity(1000, 50);
            Services.Register<ControlService>(_controlServiceInstaller);
            Services.Register<ScreenService>(_screenServiceInstaller);
            Services.Register<PoolService>();
            Services.Register<DebugService>(_debugServiceInstaller);
            Services.Register<SceneLoaderService>();
            Services.Register<StatisticService>();
            Services.Register<AudioService>(_audioServiceInstaller);
            Services.Register<SettingsService>(_settingsServiceInstaller);
            
            SystemsService.Bind<ControlSystem>();
            SystemsService.Bind<ScreenSystem>();
            SystemsService.Bind<SceneLoaderSystem>();
            SystemsService.Bind<DebugSystem>();
            SystemsService.Bind<AudioSystem>();
            
            //Services.Get<StatisticService>().AddValueToRecord(StatisticData.RecordName.Movement, 10f);
            new StartupSystemsInitializedEvent().Fire();
            StartCoroutine(LateStartup());
        }

        private void OnApplicationQuit()
        {
            Services.Dispose();
            SystemsService.Dispose();
        }
        
        private IEnumerator LateStartup()
        {
            yield return null;
            new StartupSystemsLateInitEvent().Fire();
        }
    }
}