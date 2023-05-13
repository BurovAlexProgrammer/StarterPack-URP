using _Project.Scripts.Main.AppServices;
using UnityEngine;

namespace _Project.Scripts.Main
{
    [RequireComponent(typeof(Canvas))]
    public class CameraToCanvasOnAwake : MonoBehaviour
    {
        [SerializeField] private ScreenService.CameraType _cameraType = ScreenService.CameraType.MainCamera;
        [SerializeField] private float _planeDistance = 0.1f;
        
        private void OnEnable()
        {
            var canvas = GetComponent<Canvas>();
            var screenService = Services.Get<ScreenService>();
            screenService.SetCameraToCanvas(canvas);
            canvas.planeDistance = _planeDistance;
            enabled = false;
        }
    }
}