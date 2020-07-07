using UnityEngine;

namespace Fungible.Movement
{
    [RequireComponent(typeof(Room))]
    public class RoomColoring : MonoBehaviour
    {
        [SerializeField] private Color color;
        
        private Color _cachedColor;

        private void OnEnable()
        {
            _cachedColor = Map.Instance.GetSpriteRenderer().color;
            Map.Instance.GetSpriteRenderer().color = color;
        }

        private void OnDisable()
        {
            Map.Instance.GetSpriteRenderer().color = _cachedColor;
        }
    }
}