using UnityEngine;

namespace Fungible.Movement
{
    [RequireComponent(typeof(Room))]
    public class RoomColoring : MonoBehaviour
    {
        [SerializeField] private Color color;
        
        private Color cachedColor;

        private void OnEnable()
        {
            cachedColor = Map.Instance.GetSpriteRenderer().color;
            Map.Instance.GetSpriteRenderer().color = color;
        }

        private void OnDisable()
        {
            Map.Instance.GetSpriteRenderer().color = cachedColor;
        }
    }
}