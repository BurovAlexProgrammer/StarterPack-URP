using _Project.Scripts.Main.AppServices;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main
{
    public class CameraHolder : MonoBehaviour
    {
        [Inject] private Old_ScreenService _oldScreenService;
        
        private void OnDestroy()
        {
            if (_oldScreenService == null || _oldScreenService.MainCamera == null) return;
            ReturnCameraToService();
        }

        public void SetCamera()
        {
            Debug.Log("Camera was moved to cameraHolder (Click to select CameraHolder)", this);
            var mainCameraTransform = _oldScreenService.MainCamera.transform;
            mainCameraTransform.parent = transform;
            mainCameraTransform.localPosition = Vector3.zero;
            mainCameraTransform.localRotation = Quaternion.identity;
        }

        public void ReturnCameraToService()
        {
            var mainCameraTransform = _oldScreenService.MainCamera.transform;
            Debug.Log("Camera was moved to ScreenService", _oldScreenService);
            mainCameraTransform.parent = _oldScreenService.transform;
            mainCameraTransform.localPosition = Vector3.zero;
            mainCameraTransform.localRotation = Quaternion.identity;
        }
    }
}