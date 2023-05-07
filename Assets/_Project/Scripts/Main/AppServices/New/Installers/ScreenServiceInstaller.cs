using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace _Project.Scripts.Main.AppServices
{
    public class ScreenServiceInstaller : BaseServiceInstaller
    {
        public Camera CameraMain;
        public Camera CameraUI;
        public Volume Volume;
        public GameObject InternalProfiler;
        public Toggle InternalProfilerToggle;
        public Image CameraTopFrame;
        public bool ShowProfilerOnStartup;
        public Transform CameraHolder;
    }
}