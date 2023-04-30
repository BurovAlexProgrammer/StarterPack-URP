using _Project.Scripts.Main.Game;
using Tayx.Graphy;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace _Project.Scripts.Main.AppServices
{
    public class ScreenServiceInstaller : MonoBehaviour, IServiceInstaller
    {
        public Camera CameraMain;
        public Camera CameraUI;
        public Volume Volume;
        public GameObject InternalProfiler;
        public Toggle InternalProfilerToggle;
        public Image CameraTopFrame;
        public bool ShowProfilerOnStartup;
        
        public IServiceInstaller Install()
        {
            var installer = Instantiate(this, AppContext.ServicesHierarchy.transform);
            return installer;
        }
    }
}