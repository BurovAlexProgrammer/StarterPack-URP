using System;
using _Project.Scripts.Main.AppServices;
using _Project.Scripts.Main.Wrappers;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main
{
    public class CameraHolder : MonoBehaviour
    {
        private ScreenService _screenService;

        private void Awake()
        {
            _screenService = Services.Get<ScreenService>();
        }

        private void OnDestroy()
        {
            if (_screenService == null || _screenService.CameraMain == null) return;
            ReturnCameraToService();
        }

        public void SetCamera()
        {
            Debug.Log("Camera was moved to cameraHolder (Click to select CameraHolder)", this);
            var mainCameraTransform = _screenService.CameraMain.transform;
            mainCameraTransform.parent = transform;
            mainCameraTransform.localPosition = Vector3.zero;
            mainCameraTransform.localRotation = Quaternion.identity;
        }

        public void ReturnCameraToService()
        {
            var mainCameraTransform = _screenService.CameraMain.transform;
            Log.Info("Camera was moved to ScreenService", _screenService.CameraHolder);
            mainCameraTransform.parent = _screenService.CameraHolder;
            mainCameraTransform.localPosition = Vector3.zero;
            mainCameraTransform.localRotation = Quaternion.identity;
        }
    }
}