using _Project.Scripts.Main.Settings;

namespace _Project.Scripts.Main.AppServices
{
    public class SettingsServiceInstaller : BaseServiceInstaller
    {
        public SettingGroup<VideoSettings> VideoSettings;
        public SettingGroup<AudioSettings> AudioSettings;
        public SettingGroup<GameSettings> GameSettings;
    }
}