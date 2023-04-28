using System;
using System.IO;
using _Project.Scripts.Main.AppServices;
using _Project.Scripts.Main.Events;
using _Project.Scripts.Main.Systems;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using static _Project.Scripts.Main.AppServices.Old_Services;
using AudioService = _Project.Scripts.Main.AppServices.AudioService;

namespace _Project.Scripts.Main.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoaderService _sceneLoaderServicePrefab;
        [SerializeField] private Old_ScreenService _screenServicePrefab;
        [SerializeField] private SettingsService _settingsServicePrefab;
        [SerializeField] private GameManagerService _gameManagerServicePrefab;
        [SerializeField] private LocalizationService _localizationServicePrefab;
        [SerializeField] private ControlService _controlServicePrefab;
        [SerializeField] private DebugService _debugServicePrefab;
        [SerializeField] private AudioService _audioServicePrefab;
        [SerializeField] private StatisticService _statisticServicePrefab;

        public override void InstallBindings()
        {
            gameObject.name = "Services";
            
            InstallSceneLoaderService();
            InstallScreenService();
            InstallAudioService();
            InstallGameManagerService();
            InstallSettingService();
            InstallLocalizationService();
            InstallControlService();
            InstallDebugService();
            InstallStatisticService();
            
            if (_debugServicePrefab.SaveLogToFile)
            {
                Application.logMessageReceived += LogToFile;
            }

            
            SystemsService.Bind<TestSystem>();
            
            new TestEvent(){Name = "Good"}.Fire();
            new Test2Event().Fire();
        }

        private void OnApplicationQuit()
        {
            SystemsService.Dispose();
        }

        private void InstallStatisticService()
        {
            Container
                .Bind<StatisticService>()
                .FromComponentInNewPrefab(_statisticServicePrefab)
                .WithGameObjectName("Statistic Service")
                .AsSingle()
                .OnInstantiated((ctx, instance) => SetService((StatisticService)instance))
                .NonLazy();
        }

        private void InstallAudioService()
        {
            Container
                .Bind<AudioService>()
                .FromComponentInNewPrefab(_audioServicePrefab)
                .WithGameObjectName("Audio Service")
                .AsSingle()
                .OnInstantiated((ctx, instance) => SetService((AudioService)instance))
                .NonLazy();
        }

        private void InstallDebugService()
        {
            Container
                .Bind<DebugService>()
                .FromComponentInNewPrefab(_debugServicePrefab)
                .WithGameObjectName("Debug Service")
                .AsSingle()
                .OnInstantiated((ctx, instance) => SetService((DebugService)instance))
                .NonLazy();
        }

        private void LogToFile(string condition, string stacktrace, LogType type)
        {
            var path = Application.persistentDataPath + "/log.txt";
            using var streamWriter = File.AppendText(path);
            streamWriter.WriteLine($"{condition}");
            streamWriter.WriteLine("----");
            streamWriter.WriteLine($"{stacktrace}");
            streamWriter.WriteLine("-----------------------------------------------------------------------------------------");
        }

        private void InstallControlService()
        {
            Container
                .Bind<ControlService>()
                .FromComponentInNewPrefab(_controlServicePrefab)
                .WithGameObjectName("Control Service")
                .AsSingle()
                .OnInstantiated((ctx, instance) => SetService(instance as ControlService))
                .NonLazy();
        }

        private void InstallSettingService()
        {
            Container
                .Bind<SettingsService>()
                .FromComponentInNewPrefab(_settingsServicePrefab)
                .WithGameObjectName("Settings Service")
                .AsSingle()
                .OnInstantiated((ctx, instance) =>
                {
                    var service = (SettingsService)instance;
                    service.Init();
                    service.Load();
                }).NonLazy();
        }

        private void InstallScreenService()
        {
            Container
                .Bind<Old_ScreenService>()
                .FromComponentInNewPrefab(_screenServicePrefab)
                .WithGameObjectName("Screen Service")
                .AsSingle()
                .OnInstantiated((ctx, instance) => SetService((Old_ScreenService)instance))
                .NonLazy();
        }

        private void InstallSceneLoaderService()
        {
           Container
                .Bind<SceneLoaderService>()
                .FromComponentInNewPrefab(_sceneLoaderServicePrefab)
                .WithGameObjectName("Scene Loader")
                .AsSingle()
                .OnInstantiated((ctx, instance) => SetService((SceneLoaderService)instance))
                .NonLazy();
        }

        private void InstallGameManagerService()
        {
            Container
                .Bind<GameManagerService>()
                .FromComponentInNewPrefab(_gameManagerServicePrefab)
                .WithGameObjectName("Game Manager")
                .AsSingle()
                .OnInstantiated((ctx, instance) => SetService(instance as GameManagerService))
                .NonLazy();
        }
        
        void InstallLocalizationService()
        {
            Container
                .Bind<LocalizationService>()
                .FromComponentInNewPrefab(_localizationServicePrefab)
                .WithGameObjectName("Localization Service")
                .AsSingle()
                .OnInstantiated((ctx, instance) => (instance as LocalizationService)?.Init())
                .NonLazy();
        }
    }
}
