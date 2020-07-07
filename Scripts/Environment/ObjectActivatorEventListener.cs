using Fungible.Events;
using UnityEngine;

namespace Fungible.Environment
{
    [RequireComponent(typeof(ObjectActivator))]
    public class ObjectActivatorEventListener : BaseEventListener
    {
        public override void Event()
        {
            GetComponent<ObjectActivator>().Invoke();
        }
    }
}