using _Project.Scripts.Main.AppServices;
using _Project.Scripts.Main.Events;
using ModestTree;

namespace _Project.Scripts.Main.Systems
{
    public class ScreenSystem : BaseSystem
    {
        private ScreenService ScreenService => Services.Get<ScreenService>();
        public override void Init()
        {
            base.Init();
            ScreenService.InternalProfilerToggle.isOn = ScreenService.InternalProfiler.activeSelf;
            ScreenService.InternalProfilerToggle.onValueChanged.AddListener(_ => OnInternalProfileDisplayToggle(null));
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

        public override void OnDispose()
        {
            base.OnDispose();
            Log.Info("OnDispose");
            ScreenService.InternalProfilerToggle.onValueChanged.RemoveAllListeners();
        }

        private void OnInternalProfileDisplayToggle(BaseEvent baseEvent)
        {
            ScreenService.ToggleDisplayProfiler();
        }

    }
}