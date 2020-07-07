using UnityEngine;

namespace Fungible.Storytelling
{
    public class StoryLabelObject : MonoBehaviour
    {
        [SerializeField] protected string text;
        [SerializeField] protected bool showOnce;

        private bool _showed;

        protected internal void Show()
        {
            if (showOnce && _showed)
                return;

            StoryLabelController.Instance.SetText(text);
            StoryLabelController.Instance.Show();
            _showed = true;
        }

        public void SetText(string newText)
        {
            text = newText;
        }
    }
}