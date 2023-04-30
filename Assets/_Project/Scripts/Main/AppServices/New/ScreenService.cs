using _Project.Scripts.Extension;
using Codice.Client.BaseCommands;
using Tayx.Graphy;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Project.Scripts.Main.AppServices
{
    public class ScreenService : IService, IConstructInstaller
    {
        public Camera CameraMain;
        public Volume Volume;
        private GraphyManager InternalProfiler;

        private void Foo()
        {
            //var controls = _controlService.Controls;
            //controls.Player.InternalProfiler.BindAction(BindActions.Started, ctx => ToggleShowProfiler());
            //TODO to Systems
        }

        private void ToggleShowProfiler()
        {
            InternalProfiler.enabled = !InternalProfiler.enabled;
        }

        public void Construct(IServiceInstaller installer)
        {
            var screenServiceInstaller = installer.Install() as ScreenServiceInstaller;
            InternalProfiler = screenServiceInstaller.InternalProfiler;
            CameraMain = screenServiceInstaller.CameraMain;
            Volume = screenServiceInstaller.Volume;
            InternalProfiler.enabled = screenServiceInstaller.ShowProfilerOnStartup;
        }
    }
}