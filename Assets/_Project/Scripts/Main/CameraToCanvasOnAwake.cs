using _Project.Scripts.Main.AppServices;
using UnityEngine;

namespace _Project.Scripts.Main
{
    [RequireComponent(typeof(Canvas))]
    public class CameraToCanvasOnAwake : MonoBehaviour
    {
        [SerializeField] private ScreenService.CameraType _cameraType;
        
        private void OnEnable()
        {
            var canvas = GetComponent<Canvas>();
            var screenService = Services.Get<ScreenService>();
            screenService.SetCameraToCanvas(canvas, _cameraType);
            enabled = false;
        }
    }
}