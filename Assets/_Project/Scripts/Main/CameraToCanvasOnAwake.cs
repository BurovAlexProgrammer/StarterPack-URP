using _Project.Scripts.Main.AppServices;
using UnityEngine;

namespace _Project.Scripts.Main
{
    [RequireComponent(typeof(Canvas))]
    public class CameraToCanvasOnAwake : MonoBehaviour
    {
        [SerializeField] private CameraType _cameraType;

        private enum CameraType
        {
            MainCamera,
            UiCamera
        }

        private void OnEnable()
        {
            var canvas = GetComponent<Canvas>();
            var screenService = Services.Get<ScreenService>();
            canvas.worldCamera = _cameraType == CameraType.MainCamera ? screenService.CameraMain : screenService.CameraUI;
            enabled = false;
        }
    }
}