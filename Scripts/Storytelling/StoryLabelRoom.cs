using Fungible.Movement;

namespace Fungible.Storytelling
{
    public class StoryLabelRoom : StoryLabelBaseObject
    {
        private void Awake()
        {
            GetComponent<Room>().enterEvent += Show;
        }

        private void OnDestroy()
        {
            GetComponent<Room>().enterEvent -= Show;
        }
    }
}