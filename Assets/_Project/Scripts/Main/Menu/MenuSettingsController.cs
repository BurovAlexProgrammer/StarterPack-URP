using _Project.Scripts.Main.Settings;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main.Menu
{
    public class MenuSettingsController : MonoBehaviour
    {
        // private SettingsService _settings;

        // public VideoSettings VideoSettings => _settings.Video;
        // public GameSettings GameSettings => _settings.GameSettings;
        //
        // public void Apply()
        // {
        //     _settings.Save();
        //     _settings.Apply();
        // }
        //
        // public void Save()
        // {
        //     _settings.Save();
        //     _settings.Apply();
        // }
        //
        // public void ResetToDefault()
        // {
        //     _settings.Restore();
        //     _settings.Apply();
        // }

        public void Bind(bool value, ref bool settingsValue)
        {
            settingsValue = value;
        }
    }
}