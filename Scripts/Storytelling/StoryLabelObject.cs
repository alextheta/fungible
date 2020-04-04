using Fungible.Environment;
using UnityEngine;

namespace Fungible.Storytelling
{
    [RequireComponent(typeof(ClickableObject))]
    public class StoryLabelObject : StoryLabelBaseObject
    {
        private void OnClick()
        {
            if (!GetComponent<ClickableObject>().clickable)
                return;

            Show();
        }

        private void Awake()
        {
            GetComponent<ClickableObject>().ClickEvent += OnClick;
        }
        
        private void OnDestroy()
        {
            GetComponent<ClickableObject>().ClickEvent -= OnClick;
        }
    }
}