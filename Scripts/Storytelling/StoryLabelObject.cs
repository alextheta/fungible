using System.Collections;
using Fungible.Environment;
using UnityEngine;

namespace Fungible.Storytelling
{
    [RequireComponent(typeof(ClickableObject))]
    public class StoryLabelObject : StoryLabelBaseObject
    {
        private ClickableObject clickableObject;

        private void OnClick()
        {
            StartCoroutine(ShowCoroutine());
        }

        private void Awake()
        {
            clickableObject = GetComponent<ClickableObject>();
            clickableObject.ClickEvent += OnClick;
        }
        
        private void OnDestroy()
        {
            clickableObject.ClickEvent -= OnClick;
        }

        private IEnumerator ShowCoroutine()
        {
            yield return null; /* Skip frame for click event order execution purpose */

            if (clickableObject.clickable)
                Show();
        }
    }
}