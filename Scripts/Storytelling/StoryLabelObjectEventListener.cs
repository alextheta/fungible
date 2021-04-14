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
            if (SaveController.LoadState)
            {
                return;
            }

            if (gameObject.activeSelf)
                _storyObject.Show();
        }
    }
}