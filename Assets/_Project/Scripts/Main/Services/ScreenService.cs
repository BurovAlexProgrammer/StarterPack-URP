using System;
using _Project.Scripts.Extension;
using _Project.Scripts.Main.Settings;
using _Project.Scripts.Main.Wrappers;
using Tayx.Graphy;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace _Project.Scripts.Main.Services
{
    public class ScreenService : IService, IConstructInstaller
    {
        public Action<bool> OnDebugProfilerToggleSwitched; 

        private Camera _cameraMain;
        private Volume _volume;
        private VolumeProfile _volumeProfile;
        private GameObject _internalProfiler;
        private GraphyManager _internalProfilerManager;
        private Toggle _internalProfilerToggle;
        private Transform _cameraHolder;

        public enum CameraType
        {
            MainCamera,
            //UiCamera
        }
        
        public void ToggleDisplayProfiler()
        {
            _internalProfiler.gameObject.SwitchActive();
            _internalProfilerToggle.SetIsOnWithoutNotify(_internalProfiler.gameObject.activeSelf);
            _internalProfilerToggle.gameObject.SetActive(false);
            _internalProfilerToggle.gameObject.SetActive(true);
        }

        public void Construct(IServiceInstaller installer)
        {
            var screenServiceInstaller = installer.Install() as ScreenServiceInstaller;
            _internalProfiler = screenServiceInstaller.InternalProfilerPanels;
            _cameraMain = screenServiceInstaller.CameraMain;
            _volume = screenServiceInstaller.Volume;
            _volumeProfile = _volume.profile;
            _internalProfilerManager = screenServiceInstaller.InternalProfilerManager;
            _internalProfilerToggle = screenServiceInstaller.InternalProfilerToggle;
            _internalProfiler.SetActive(screenServiceInstaller.ShowProfilerOnStartup);
            _cameraHolder = screenServiceInstaller.CameraHolder;

            _internalProfilerToggle.isOn = _internalProfiler.activeSelf;
            _internalProfilerToggle.onValueChanged.AddListener(OnProfilerToggleSwitched);
        }

        private void OnProfilerToggleSwitched(bool value)
        {
            OnDebugProfilerToggleSwitched?.Invoke(value);
        }

        ~ScreenService()
        {
            _internalProfilerToggle.onValueChanged.RemoveListener(OnProfilerToggleSwitched);
        }
        
        public void ActiveProfileVolume<T>(bool active) where T : IPostProcessComponent
        {
            var type = typeof(T);
            if (_volumeProfile.TryGet(type, out VolumeComponent volumeComponent))
            {
                volumeComponent.active = active;
                return;
            }

            throw new Exception($"VolumeProfile {type.FullName} not found.");
        }
        
        public void SetCameraPlace(Transform parent)
        {
            Log.Info("Camera was moved to cameraHolder (Click to select CameraHolder)", parent);
            var mainCameraTransform = _cameraMain.transform;
            mainCameraTransform.parent = parent;
            mainCameraTransform.localPosition = Vector3.zero;
            mainCameraTransform.localRotation = Quaternion.identity;
        }

        public void ReturnCameraToService()
        {
            var mainCameraTransform = _cameraMain.transform;
            Log.Info("Camera was moved to ScreenService", _cameraHolder);
            mainCameraTransform.parent = _cameraHolder;
            mainCameraTransform.localPosition = Vector3.zero;
            mainCameraTransform.localRotation = Quaternion.identity;
        }

        public void SetCameraToCanvas(Canvas canvas)
        {
            canvas.worldCamera = _cameraMain;
        }

        public void ApplySettings(VideoSettings videoSettings)
        {
            ActiveProfileVolume<Bloom>(videoSettings.PostProcessBloom);
            ActiveProfileVolume<DepthOfField>(videoSettings.PostProcessDepthOfField);
            ActiveProfileVolume<Vignette>(videoSettings.PostProcessVignette);
            ActiveProfileVolume<FilmGrain>(videoSettings.PostProcessFilmGrain);
            ActiveProfileVolume<MotionBlur>(videoSettings.PostProcessMotionBlur);
            ActiveProfileVolume<LensDistortion>(videoSettings.PostProcessLensDistortion);
            var additionalCameraSettings = _cameraMain.GetComponent<UniversalAdditionalCameraData>();
            additionalCameraSettings.antialiasing = videoSettings.PostProcessAntiAliasing ? AntialiasingMode.FastApproximateAntialiasing : AntialiasingMode.None;
            //var t1 = GraphicsSettings.GetGraphicsSettings();
            //GraphicsSettings.GetSettingsForRenderPipeline<>()
        }

        public void SetupInternalProfiler(AudioListener audioListener)
        {
            _internalProfilerManager.AudioListener = audioListener;
        }
        
        public void SetAudioListenerToCamera(AudioListener audioListener)
        {
           audioListener.transform.SetParent(_cameraMain.transform);
           audioListener.transform.localPosition = Vector3.zero;
        }
    }
}