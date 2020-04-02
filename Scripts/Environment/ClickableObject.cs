using UnityEngine;

namespace Fungible.Environment
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class ClickableObject : MonoBehaviour
    {
        public delegate void ClickAction();
        public event ClickAction ClickEvent;
        
        public void OnClick()
        {
            ClickEvent?.Invoke();
        }
    }
}