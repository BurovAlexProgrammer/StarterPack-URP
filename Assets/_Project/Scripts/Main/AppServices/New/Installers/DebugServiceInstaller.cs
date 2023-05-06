using System;
using _Project.Scripts.Extension;
using UnityEngine;
using AppContext = _Project.Scripts.Main.Game.AppContext;

namespace _Project.Scripts.Main.AppServices
{
    public class DebugServiceInstaller : MonoBehaviour, IServiceInstaller
    {
        public DebugServiceConfig Config;

        public IServiceInstaller Install()
        {
            var installer = Instantiate(this, AppContext.ServicesHierarchy);
            installer.gameObject.CleanName();
            return installer;
        }
    }
}