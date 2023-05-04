
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace _Project.Scripts.Extension
{
    public class UnityEditorUtility
    {
        public static void CollapseHierarchyItem(Object gameObject)
        {
#if UNITY_EDITOR
            SetCollapseHierarchyItem(gameObject);
#endif
        }
        public static void ExpandHierarchyItem(Object gameObject)
        {
#if UNITY_EDITOR
            SetCollapseHierarchyItem(gameObject, false);
#endif
        }

        public static void ExpandScene(Scene scene)
        {
#if UNITY_EDITOR
            var hierarchy = GetFocusedWindow("Hierarchy");

            if (hierarchy == null)
            {
                Debug.LogWarning("Hierarchy window ");
                return;
            }

            var list = new List<GameObject>();
            scene.GetRootGameObjects(list);

            Selection.activeObject = list[0];
            
            var key = new Event { keyCode = KeyCode.UpArrow, type = EventType.KeyDown };
            hierarchy.SendEvent(key);
            
            key = new Event { keyCode =  KeyCode.RightArrow , type = EventType.KeyDown };
            hierarchy.SendEvent(key);
#endif
        }
        
#if UNITY_EDITOR
        private static void SetCollapseHierarchyItem(Object gameObject, bool collapse = true)
        {
            var hierarchy = GetFocusedWindow("Hierarchy");

            if (hierarchy == null)
            {
                Debug.LogWarning("Hierarchy window ");
                return;
            }
            
            Selection.activeObject = gameObject;
            var key = new Event { keyCode = collapse ? KeyCode.LeftArrow : KeyCode.RightArrow, type = EventType.KeyDown };
            hierarchy.SendEvent(key);
        }

        public static EditorWindow GetFocusedWindow(string window)
        {
            FocusOnWindow(window);
            return EditorWindow.focusedWindow;
        }
        
        public static void FocusOnWindow(string window)
        {
            EditorApplication.ExecuteMenuItem("Window/General/" + window);
        }
#endif
    }
}