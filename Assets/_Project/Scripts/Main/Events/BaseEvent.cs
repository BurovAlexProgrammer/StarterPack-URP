using _Project.Scripts.Main.Services;

namespace _Project.Scripts.Main.Events
{
    public abstract class BaseEvent
    {
        public void Fire()
        {
            SystemsService.FireEvent(this);
        }
    }
}