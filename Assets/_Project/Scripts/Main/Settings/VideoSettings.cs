using System;
using _Project.Scripts.Main.Services;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace _Project.Scripts.Main.Settings
{
    [Serializable]
    [CreateAssetMenu(menuName = "Custom/Settings/Video Settings")]
    public class VideoSettings: SettingsSO
    {
        public bool PostProcessAntiAliasing; //TODO impl
        public bool PostProcessBloom;
        public bool PostProcessVignette;
        public bool PostProcessAmbientOcclusion; //TODO impl
        public bool PostProcessDepthOfField;
        public bool PostProcessFilmGrain;
        public bool PostProcessLensDistortion;
        public bool PostProcessMotionBlur;

        private VolumeProfile _volumeProfile;
        private VolumeComponent _volumeComponent;

        public override void ApplySettings(SettingsService settingsService)
        {
            Services.Services.Get<ScreenService>().ApplySettings(this);
        }
    }
}