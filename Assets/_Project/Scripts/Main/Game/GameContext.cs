using System;
using _Project.Scripts.Main.AppServices;
using UnityEngine;

namespace _Project.Scripts.Main.Game
{
    public class GameContext : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void OnApplicationQuit()
        {
            Services.Clear();
        }
    }
}