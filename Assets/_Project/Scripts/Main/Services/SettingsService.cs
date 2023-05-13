using System.Collections.Generic;
using _Project.Scripts.Main.Services;
using _Project.Scripts.Main.Settings;
using AudioSettings = _Project.Scripts.Main.Settings.AudioSettings;

namespace _Project.Scripts.Main.Services
{
    public class SettingsService : IService, IConstructInstaller
    {
        private SettingGroup<VideoSettings> _videoSettings;
        private SettingGroup<AudioSettings> _audioSettings;
        private SettingGroup<GameSettings> _gameSettings;

        private ScreenService _oldScreenService;
        
        private List<ISettingGroup> _settingList;

        public VideoSettings Video => _videoSettings.CurrentSettings;
        public AudioSettings Audio => _audioSettings.CurrentSettings;
        public GameSettings GameSettings => _gameSettings.CurrentSettings;


        public void Construct(IServiceInstaller installer)
        {
            var settingsServiceInstaller = installer as SettingsServiceInstaller;

            _videoSettings = settingsServiceInstaller.VideoSettings;
            _audioSettings = settingsServiceInstaller.AudioSettings;
            _gameSettings = settingsServiceInstaller.GameSettings;
            
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