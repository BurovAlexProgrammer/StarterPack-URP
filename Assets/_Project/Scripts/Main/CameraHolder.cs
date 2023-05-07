using _Project.Scripts.Main.AppServices;
using UnityEngine;

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
            ReturnCameraToService();
        }

        public void SetCamera()
        {
            _screenService.SetCameraPlace(transform);
        }

        public void ReturnCameraToService()
        {
            _screenService.ReturnCameraToService();
        }
    }
}