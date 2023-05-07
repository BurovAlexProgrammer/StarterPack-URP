using _Project.Scripts.Extension;
using _Project.Scripts.Main.Game;
using _Project.Scripts.Main.Settings;
using UnityEngine;
using AudioSettings = _Project.Scripts.Main.Settings.AudioSettings;


namespace _Project.Scripts.Main.AppServices
{
    public class SettingsServiceInstaller : MonoBehaviour, IServiceInstaller
    {
        public SettingGroup<VideoSettings> VideoSettings;
        public SettingGroup<AudioSettings> AudioSettings;
        public SettingGroup<GameSettings> GameSettings;
        
        public IServiceInstaller Install()
        {
            var installer = Instantiate(this, AppContext.ServicesHierarchy);
            installer.gameObject.CleanName();
            return installer;
        }
    }
}