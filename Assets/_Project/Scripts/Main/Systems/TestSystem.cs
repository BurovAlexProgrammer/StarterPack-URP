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
        }

        private void TestCallback(IEvent obj)
        {
            throw new System.NotImplementedException();
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