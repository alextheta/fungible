using UnityEngine;

namespace Fungible
{
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class ClickableObject : MonoBehaviour
    {
        public abstract void OnClick();
    }
}