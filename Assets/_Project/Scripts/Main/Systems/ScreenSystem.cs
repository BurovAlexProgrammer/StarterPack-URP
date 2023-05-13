using _Project.Scripts.Main.Events;
using _Project.Scripts.Main.Services;

namespace _Project.Scripts.Main.Systems
{
    public class ScreenSystem : BaseSystem
    {
        private ScreenService ScreenService;
        public override void Init()
        {
            base.Init();
            ScreenService = Services.Services.Get<ScreenService>();
            ScreenService.OnDebugProfilerToggleSwitched += OnDebugProfilerToggleSwitched;
        }

        private void OnDebugProfilerToggleSwitched(bool value)
        {
            new ToggleInternalProfileEvent().Fire();
        }

        public override void AddEventHandlers()
        {
            base.AddEventHandlers();
            AddListener<ToggleInternalProfileEvent>(OnInternalProfileDisplayToggle);
        }

        public override void RemoveEventHandlers()
        {
            base.RemoveEventHandlers();
            RemoveListener<ToggleInternalProfileEvent>();
        }

        private void OnInternalProfileDisplayToggle(BaseEvent baseEvent)
        {
            ScreenService.ToggleDisplayProfiler();
        }

    }
}