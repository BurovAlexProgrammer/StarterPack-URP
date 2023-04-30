using _Project.Scripts.Main.AppServices;
using UnityEngine;

namespace _Project.Scripts.Main.Game
{
    public class AppContext : MonoBehaviour
    {
        public static GameObject Hierarchy;
        public static GameObject ServicesHierarchy;

        public static AppContext Instantiate()
        {
            Hierarchy = Instantiate(new GameObject());
            Hierarchy.name = "AppContext";
            var appContext = Hierarchy.AddComponent<AppContext>();
            ServicesHierarchy = Instantiate(new GameObject(), Hierarchy.transform);
            ServicesHierarchy.name = "Services";

            return appContext;
        }
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void OnApplicationQuit()
        {
            Services.Dispose();
            SystemsService.Dispose();
        }
    }
}