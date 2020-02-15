using UnityEngine;

namespace Fungible.Environment
{
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class ClickableObject : MonoBehaviour
    {
        public abstract void OnClick();
    }
}