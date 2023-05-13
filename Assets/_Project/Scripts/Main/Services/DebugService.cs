using System.IO;
using _Project.Scripts.Main.Services;
using UnityEngine;

namespace _Project.Scripts.Main.Services
{
    public class DebugService : IService, IConstructInstaller
    {
        private DebugServiceConfig _serviceConfig;
        private Transform _gizmosContainer;
        
        public bool IsSaveLogToFile => _serviceConfig.SaveLogToFile;
        
        public void Construct(IServiceInstaller installer)
        {
            var debugInstaller = installer as DebugServiceInstaller;
            _serviceConfig = debugInstaller.Config;
            _gizmosContainer = debugInstaller.transform;
            
            if (_serviceConfig.SaveLogToFile)
            {
                Application.logMessageReceived += LogToFile;
            }
        }
        
        private void LogToFile(string condition, string stacktrace, LogType type)
        {
            var path = Application.persistentDataPath + "/log.txt";
            using var streamWriter = File.AppendText(path);
            streamWriter.WriteLine($"{condition}");
            streamWriter.WriteLine("----");
            streamWriter.WriteLine($"{stacktrace}");
            streamWriter.WriteLine("-----------------------------------------------------------------------------------------");
        }
    }
}