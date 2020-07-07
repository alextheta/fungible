using System.Collections;
using System.ComponentModel;
using Fungible.Environment;
using Fungible.Events;
using UnityEngine;

namespace Fungible.Storytelling
{
    [RequireComponent(typeof(StoryLabelObject))]
    public class StoryLabelObjectEventListener : BaseEventListener
    {
        private StoryLabelObject _storyObject;

        private void Awake()
        {
            _storyObject = GetComponent<StoryLabelObject>();
        }

        public override void Event()
        {
            if (gameObject.activeSelf)
                StartCoroutine(ShowCoroutine());
        }

        private IEnumerator ShowCoroutine()
        {
            yield return null; /* Skip frame for click event order execution purpose */

            _storyObject.Show();
        }
    }
}