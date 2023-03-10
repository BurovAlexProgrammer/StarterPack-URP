using _Project.Scripts.Main.AppServices;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main
{
    public class CameraHolder : MonoBehaviour
    {
        [Inject] private ScreenService _screenService;
        
        private void OnDestroy()
        {
            if (_screenService == null || _screenService.MainCamera == null) return;
            ReturnCameraToService();
        }

        public void SetCamera()
        {
            Debug.Log("Camera was moved to cameraHolder (Click to select CameraHolder)", this);
            var mainCameraTransform = _screenService.MainCamera.transform;
            mainCameraTransform.parent = transform;
            mainCameraTransform.localPosition = Vector3.zero;
            mainCameraTransform.localRotation = Quaternion.identity;
        }

        public void ReturnCameraToService()
        {
            var mainCameraTransform = _screenService.MainCamera.transform;
            Debug.Log("Camera was moved to ScreenService", _screenService);
            mainCameraTransform.parent = _screenService.transform;
            mainCameraTransform.localPosition = Vector3.zero;
            mainCameraTransform.localRotation = Quaternion.identity;
        }
    }
}