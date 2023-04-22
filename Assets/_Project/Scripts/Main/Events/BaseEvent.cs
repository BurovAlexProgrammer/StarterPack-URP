using _Project.Scripts.Main.AppServices;

namespace _Project.Scripts.Main.Events
{
    public class BaseEvent : IEvent
    {
        public void Fire()
        {
            SystemsService.FireEvent(this);
        }
    }
}