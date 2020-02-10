using UnityEngine;

namespace Fungible.UI
{
    public class MainPanel : MonoBehaviour
    {
        public Vector2 GetWorldSize()
        {
            RectTransform rt = GetComponent<RectTransform>();

            Vector3[] v = new Vector3[4];
            rt.GetWorldCorners(v);

            Vector2 bottomLeft = v[0];
            Vector2 topLeft = v[1];
            Vector2 topRight = v[2];

            Vector2 result = new Vector2(Vector2.Distance(topLeft, topRight), 
                                         Vector2.Distance(topLeft, bottomLeft));

            return result;
        }
    }
}