using System;
using UnityEngine;

namespace _Project.Scripts.Main.Wrappers
{
    public static class Log
    {
        public static void Error(string message, Type type = Type.Default)
        {
            Debug.LogError(message);
        }
        
        public static void Info(string message, Type type = Type.Default)
        {
            Debug.Log(message);
        }        
        
        public static void Warn(string message, Type type = Type.Default)
        {
            Debug.LogWarning(message);
        }

        public static void Exception(Exception exception)
        {
            Debug.LogException(exception);
        }
        
        public enum Type
        {
            Default,
            Service,
        }
    }
}