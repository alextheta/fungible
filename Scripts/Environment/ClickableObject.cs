using UnityEngine;

namespace Fungible.Environment
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class ClickableObject : MonoBehaviour
    {
        public delegate void ClickAction();
        public event ClickAction ClickEvent;
        [SerializeField] public bool clickable;
        
        public void OnClick()
        {
            if (!clickable)
                return;

            ClickEvent?.Invoke();
        }
    }
}