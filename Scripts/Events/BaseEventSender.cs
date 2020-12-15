using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Fungible.Events
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SortingGroup))]
    public class BaseEventSender : MonoBehaviour
    {
        [SerializeField] private List<BaseEventListener> eventListeners;

        public void Subscribe(BaseEventListener listener)
        {
            if (eventListeners != null)
            {
                eventListeners.Add(listener);
            }
            else
            {
                Debug.LogError(this + " Subscribe: Event Listener list is null");
            }
        }

        public void Unsubscribe(BaseEventListener listener)
        {
            if (eventListeners != null)
            {
                eventListeners.Remove(listener);
            }
            else
            {
                Debug.LogError(this + " Unsubscribe: Event Listener list is null");
            }
        }

        public void Invoke()
        {
            if (eventListeners == null)
            {
                Debug.LogError(this + " Invoke: Event Listener list is null");
                return;
            }

            foreach (BaseEventListener listener in eventListeners)
            {
                if (listener)
                {
                    listener.Event();
                }
                else
                {
                    Debug.LogError(this + ": Invoke: Listener is null");
                }
            }
        }
    }
}