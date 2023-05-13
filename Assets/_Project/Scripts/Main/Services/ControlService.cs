using UnityEngine;

namespace _Project.Scripts.Main.Services
{
    public class ControlService: IService, IConstructInstaller
    {
        public Controls Controls { get; private set; }
        public CursorLockMode CursorLockState => Cursor.lockState;

        public void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.None;
        }

        public void Construct(IServiceInstaller installer)
        {
            Controls = new Controls();
            var controlInstaller = installer.Install() as ControlServiceInstaller;
        }
    }
}