using _Project.Scripts.Main.AppServices;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main
{
    [RequireComponent(typeof(Canvas))]
    public class CameraToCanvasOnAwake : MonoBehaviour
    {
        [SerializeField] private CameraTypes _camera;

        [Inject] private Old_ScreenService _oldScreenService;

        private enum CameraTypes
        {
            MainCamera,
            UiCamera
        }

        private void OnEnable()
        {
            var canvas = GetComponent<Canvas>();
            canvas.worldCamera = _camera == CameraTypes.MainCamera ? _oldScreenService.MainCamera : null;
            enabled = false;
        }
    }
}