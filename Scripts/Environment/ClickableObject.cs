using System.Diagnostics.Tracing;
using UnityEngine;

namespace Fungible.Environment
{
    [RequireComponent(typeof(ClickEventSender))]
    public abstract class ClickableObject : EventListener
    {
        private ClickEventSender _clickEventSender;
    
        /*protected void Awake()
        {
            _clickEventSender = GetComponent<ClickEventSender>();
            _clickEventSender.Subscribe(this);
        }
    
        protected void OnDestroy()
        {
            _clickEventSender.Unsubscribe(this);
        }*/
    }
}