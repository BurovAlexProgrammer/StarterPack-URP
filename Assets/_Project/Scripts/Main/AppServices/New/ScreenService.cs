using _Project.Scripts.Extension;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace _Project.Scripts.Main.AppServices
{
    public class ScreenService : IService, IConstructInstaller
    {
        public Camera CameraMain;
        public Camera CameraUI;
        public Volume Volume;
        public GameObject InternalProfiler;
        public Toggle InternalProfilerToggle;
        public Transform CameraHolder;
        
        public void ToggleDisplayProfiler()
        {
            InternalProfiler.gameObject.SwitchActive();
            InternalProfilerToggle.SetIsOnWithoutNotify(InternalProfiler.gameObject.activeSelf);
            InternalProfilerToggle.gameObject.SetActive(false);
            InternalProfilerToggle.gameObject.SetActive(true);
        }

        public void Construct(IServiceInstaller installer)
        {
            var screenServiceInstaller = installer.Install() as ScreenServiceInstaller;
            InternalProfiler = screenServiceInstaller.InternalProfiler;
            CameraMain = screenServiceInstaller.CameraMain;
            CameraUI = screenServiceInstaller.CameraUI;
            Volume = screenServiceInstaller.Volume;
            InternalProfilerToggle = screenServiceInstaller.InternalProfilerToggle;
            InternalProfiler.SetActive(screenServiceInstaller.ShowProfilerOnStartup);
            CameraHolder = screenServiceInstaller.CameraHolder;
        }
    }
}