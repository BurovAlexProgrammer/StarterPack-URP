using _Project.Scripts.Extension;
using _Project.Scripts.Main.Game;
using UnityEngine;

namespace _Project.Scripts.Main.AppServices
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