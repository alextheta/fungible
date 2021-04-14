using UnityEngine;

namespace Fungible.UI
{
    public class MainPanel : MonoBehaviour
    {
        public Vector2 GetWorldSize()
        {
            var rt = GetComponent<RectTransform>();

            var v = new Vector3[4];
            rt.GetWorldCorners(v);

            Vector2 bottomLeft = v[0];
            Vector2 topLeft = v[1];
            Vector2 topRight = v[2];

            var result = new Vector2(
                Vector2.Distance(topLeft, topRight),
                Vector2.Distance(topLeft, bottomLeft));

            return result;
        }
    }
}