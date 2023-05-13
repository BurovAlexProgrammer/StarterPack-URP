using _Project.Scripts.Main.Services;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Scripts.Main
{
    [RequireComponent(typeof(Canvas))]
    public class CameraToCanvasOnAwake : MonoBehaviour
    {
        [SerializeField][UsedImplicitly] private ScreenService.CameraType _cameraType = ScreenService.CameraType.MainCamera;
        [SerializeField] private float _planeDistance = 0.1f;
        
        private void OnEnable()
        {
            var canvas = GetComponent<Canvas>();
            var screenService = Services.Services.Get<ScreenService>();
            screenService.SetCameraToCanvas(_cameraType == ScreenService.CameraType.MainCamera ? canvas : null);
            canvas.planeDistance = _planeDistance;
            enabled = false;
        }
    }
}