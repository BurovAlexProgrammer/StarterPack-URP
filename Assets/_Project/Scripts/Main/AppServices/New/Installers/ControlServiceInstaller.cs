using _Project.Scripts.Extension;
using _Project.Scripts.Main.Game;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

namespace _Project.Scripts.Main.AppServices
{
    public class ControlServiceInstaller : MonoBehaviour, IServiceInstaller
    {
        public EventSystem EventSystemSystem;
        public InputSystemUIInputModule InputSystemUIInputModule;
        
        public IServiceInstaller Install()
        {
            var installer = Instantiate(this, AppContext.ServicesHierarchy.transform);
            installer.gameObject.CleanName();
            return installer;
        }
    }
}