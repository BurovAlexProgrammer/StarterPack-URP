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
        private ControlService _controlService;

        public override void Init()
        {
            base.Init();
            _controlService = Services.Get<ControlService>();
            _controlService.Controls.Player.InternalProfiler.BindAction(BindActions.Started, OnPressInternalProfile);
            _controlService.Controls.Enable();
        }

        public override void OnDispose()
        {
            _controlService.Controls.Player.InternalProfiler.UnbindAction(BindActions.Started, OnPressInternalProfile);
            base.OnDispose();
        }

        public override void RemoveEventHandlers()
        {
            base.RemoveEventHandlers();
            RemoveListener<PlayGameEvent>();
        }

        public override void AddEventHandlers()
        {
            base.AddEventHandlers();
            AddListener<PlayGameEvent>(OnPlayGame);
        }

        private void OnPlayGame(BaseEvent obj)
        {
            _controlService.LockCursor();
            _controlService.Controls.Player.Enable();
            _controlService.Controls.Menu.Disable();
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