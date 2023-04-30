using _Project.Scripts.Extension;
using _Project.Scripts.Main.AppServices;
using _Project.Scripts.Main.Events;
using ModestTree;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Main.Systems
{
    public class ControlSystem : BaseSystem, IPointerClickHandler 
    {
        public override void Init()
        {
            base.Init();
            var controlService = Services.Get<ControlService>();
            controlService.Controls.Player.InternalProfiler.BindAction(BindActions.Started, OnPressInternalProfile);
            controlService.Controls.Enable();
        }

        private void OnPressInternalProfile(InputAction.CallbackContext obj)
        {
            new ToggleInternalProfileEvent().Fire();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Log.Info("Clicked");
        }
    }
}