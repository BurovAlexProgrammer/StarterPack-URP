using _Project.Scripts.Extension;
using _Project.Scripts.Main.Events;
using _Project.Scripts.Main.Services;
using _Project.Scripts.Main.Wrappers;
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
            _controlService = Services.Services.Get<ControlService>();
            _controlService.Controls.Enable();
            _controlService.BindAction(BindActions.Started, OnPressInternalProfile);
        }

        public override void OnDispose()
        {
            _controlService.UnbindAction(BindActions.Started, OnPressInternalProfile);
            base.OnDispose();
        }

        public override void RemoveEventHandlers()
        {
            base.RemoveEventHandlers();
            RemoveListener<PlayGameEvent>();
            RemoveListener<ShowMainMenuEvent>();
            RemoveListener<GoToMainMenuEvent>();
        }

        public override void AddEventHandlers()
        {
            base.AddEventHandlers();
            AddListener<PlayGameEvent>(OnPlayGame);
            AddListener<ShowMainMenuEvent>(OnAnyMenu);
            AddListener<GoToMainMenuEvent>(OnAnyMenu);
        }

        private void OnAnyMenu(BaseEvent obj)
        {
            _controlService.SetMenuMode();
            new ControlModeChangedEvent()
            {
                MenuMode = _controlService.MenuMode
            }.Fire();
        }

        private void OnPlayGame(BaseEvent obj)
        {
            _controlService.SetPlayMode();
            new ControlModeChangedEvent()
            {
                MenuMode = _controlService.MenuMode
            }.Fire();
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