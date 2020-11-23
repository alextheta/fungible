using UnityEngine;

namespace Fungible.Storytelling
{
    public class StoryLabelObject : MonoBehaviour
    {
        public bool showOnce;
        [HideInInspector] public bool showed;
        [SerializeField] protected string text;

        protected internal void Show()
        {
            if (showOnce)
            {
                if (showed)
                {
                    return;
                }

                SaveController.SaveStoryLabel(this);
            }

            StoryLabelController.Instance.SetText(text);
            StoryLabelController.Instance.Show();
            showed = true;
        }

        public void SetText(string newText)
        {
            text = newText;
        }
    }
}