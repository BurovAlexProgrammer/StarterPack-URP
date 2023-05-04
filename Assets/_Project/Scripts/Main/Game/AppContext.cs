using System;
using _Project.Scripts.Extension;
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
            Hierarchy = new GameObject() {name = "AppContext"};
            var appContext = Hierarchy.AddComponent<AppContext>();
            ServicesHierarchy = new GameObject() {name = "Services"};
            ServicesHierarchy.transform.SetParent(Hierarchy.transform);
             
            return appContext;
        }
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

        }

        private void Start()
        {
            UnityEditorUtility.ExpandScene(Hierarchy.scene);
            UnityEditorUtility.ExpandHierarchyItem(Hierarchy);
            UnityEditorUtility.ExpandHierarchyItem(ServicesHierarchy);
            UnityEditorUtility.ExpandHierarchyItem(ServicesHierarchy);
        }

        private void OnApplicationQuit()
        {
            Services.Dispose();
            SystemsService.Dispose();
        }
    }
}