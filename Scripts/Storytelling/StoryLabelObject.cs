using Fungible.Environment;
using UnityEngine;

namespace Fungible.Storytelling
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class StoryLabelObject : StoryLabelBaseObject, IClickableObject 
    {
        public void OnClick()
        {
            Show();
        }
    }
}