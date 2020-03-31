using UnityEngine;

namespace Fungible.Storytelling
{
    public class StoryLabelBaseObject : MonoBehaviour
    {
        [SerializeField] protected string text;
        [SerializeField] protected bool showOnce;
        private bool showed;
        
        protected void Show()
        {
            if (showOnce && showed)
                return;

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
