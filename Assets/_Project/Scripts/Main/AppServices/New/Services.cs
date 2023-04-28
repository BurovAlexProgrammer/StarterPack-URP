using System;
using System.Collections.Generic;
using System.Linq;

namespace _Project.Scripts.Main.AppServices
{
    public class Services
    {
        private static Dictionary<Type, IService> _registeredServices = new Dictionary<Type, IService>();

        public static void Register<T>(T newService) where T : IService
        {
            if (_registeredServices.ContainsKey(typeof(T)))
            {
                throw new Exception($"Service type of {typeof(T).Name} registered already");
            }
            
            _registeredServices.Add(typeof(T), newService);
        }

        public static IService Get<T>() where T : IService
        {
            if (_registeredServices.ContainsKey(typeof(T)) == false)
            {
                throw new Exception($"Service type of {typeof(T).Name} not found.");
            }
            
            return _registeredServices[typeof(T)];
        }

        public static void Clear()
        {
            foreach (var type in _registeredServices.Keys.ToList())
            {
                _registeredServices[type] = null;
            }
            
            _registeredServices.Clear();
        }
    }
}