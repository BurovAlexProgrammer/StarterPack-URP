using System.Collections.Generic;
using _Project.Scripts.Main.Settings;
using UnityEngine;
using AudioSettings = _Project.Scripts.Main.Settings.AudioSettings;

namespace _Project.Scripts.Main.AppServices
{
    public class SettingsService : IService, IConstruct
    {
        [SerializeField] private SettingGroup<VideoSettings> _videoSettings;
        [SerializeField] private SettingGroup<AudioSettings> _audioSettings;
        [SerializeField] private SettingGroup<GameSettings> _gameSettings;

        // [Inject] private AudioService _audioService;
        private ScreenService _oldScreenService;
        
        
        private List<ISettingGroup> _settingList;

        public VideoSettings Video => _videoSettings.CurrentSettings;
        public AudioSettings Audio => _audioSettings.CurrentSettings;
        public GameSettings GameSettings => _gameSettings.CurrentSettings;

        // public AudioService AudioService => _audioService;
        // public Old_ScreenService OldScreenService => _oldScreenService;

        public void Construct()
        {
            _settingList = new List<ISettingGroup>
            {
                _audioSettings, 
                _videoSettings,
                _gameSettings,
            };

            foreach (var settingGroup in _settingList)
            {
                settingGroup.Init(this);
            }
        }

        public void Load()
        {
            foreach (var settingGroup in _settingList)
            {
                settingGroup.LoadFromFile();
            }
        }

        public void Save()
        {
            foreach (var settingGroup in _settingList)
            {
                settingGroup.SaveToFile();
                settingGroup.Init(this);
                settingGroup.LoadFromFile();
            }
        }

        public void Cancel()
        {
            foreach (var settingGroup in _settingList)
            {
                settingGroup.Cancel();
            }
        }

        public void Restore()
        {
            foreach (var settingGroup in _settingList)
            {
                settingGroup.Restore();
            }
        }

        public void Apply()
        {
            foreach (var settingGroup in _settingList)
            {
                settingGroup.ApplySettings(this);
            }
        }
    }
}