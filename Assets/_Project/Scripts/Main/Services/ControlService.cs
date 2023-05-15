using System;
using _Project.Scripts.Extension;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Main.Services
{
    public class ControlService: IService, IConstructInstaller
    {
        public Controls Controls { get; private set; }
        public CursorLockMode CursorLockState => Cursor.lockState;
        public bool MenuMode => _menuMode;
        
        private bool _menuMode;

        public void SetPlayMode()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _menuMode = false;
            Controls.Player.Enable();
            Controls.Menu.Disable();
        }

        public void SetMenuMode()
        {
            Cursor.lockState = CursorLockMode.None;
            _menuMode = true;
            Controls.Player.Disable();
            Controls.Menu.Enable();
        }

        public void Construct(IServiceInstaller installer)
        {
            Controls = new Controls();
            var controlInstaller = installer.Install() as ControlServiceInstaller;
        }

        public void BindAction(BindActions action, Action<InputAction.CallbackContext> callback)
        {
            Controls.Player.InternalProfiler.BindAction(action, callback);
        }
        
        public void UnbindAction(BindActions action, Action<InputAction.CallbackContext> callback)
        {
            Controls.Player.InternalProfiler.UnbindAction(action, callback);
        }
    }
}