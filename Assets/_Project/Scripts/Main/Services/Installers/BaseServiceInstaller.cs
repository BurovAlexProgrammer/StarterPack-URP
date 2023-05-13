using _Project.Scripts.Extension;
using _Project.Scripts.Main.Game;
using _Project.Scripts.Main.Services;
using UnityEngine;

namespace _Project.Scripts.Main.Services
{
    public abstract class BaseServiceInstaller: MonoBehaviour, IServiceInstaller
    {
        public virtual IServiceInstaller Install()
        {
            var installer = Instantiate(this, AppContext.ServicesHierarchy);
            installer.gameObject.CleanName();
            return installer;
        }
    }
}