using System;
using _Project.Scripts.Main.AppServices;
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
            var screenService = Services.Get<ScreenService>();
            // _volumeProfile = settingsService.OldScreenService.VolumeProfile;
            screenService.SetProfileVolume(typeof(Bloom), PostProcessBloom);
            screenService.SetProfileVolume(typeof(DepthOfField), PostProcessDepthOfField);
            screenService.SetProfileVolume(typeof(Vignette), PostProcessVignette);
            screenService.SetProfileVolume(typeof(FilmGrain), PostProcessFilmGrain);
            screenService.SetProfileVolume(typeof(MotionBlur), PostProcessMotionBlur);
            screenService.SetProfileVolume(typeof(LensDistortion), PostProcessLensDistortion);
            // postProcessLayer.antialiasingMode = PostProcessAntiAliasing ? PostProcessLayer.Antialiasing.FastApproximateAntialiasing : None;
        }
    }

    public static class VideoSettingsAttributes
    {
    }
}