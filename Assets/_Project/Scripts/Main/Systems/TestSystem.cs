using _Project.Scripts.Main.Events;
using UnityEngine;

namespace _Project.Scripts.Main.Systems
{
    public class TestSystem : BaseSystem
    {
        public override void Init()
        {
            Debug.Log("Service has bound.");
            base.Init();
        }

        public override void AddEventHandlers()
        {
            base.AddEventHandlers();
            AddListener<TestEvent>(TestCallback);
            AddListener<Test2Event>(TestCallback2);
        }

        private void TestCallback(BaseEvent sourceEvent)
        {
            var ev = sourceEvent as TestEvent;
            Debug.Log("Event Name: "+ev.Name);
        }
        
        private void TestCallback2(BaseEvent sourceEvent)
        {
            Debug.Log("Event Name: unknown");
        }

        public override void RemoveEventHandlers()
        {
            base.RemoveEventHandlers();
        }

        private void TestListen()
        {
            
        }
    }
}