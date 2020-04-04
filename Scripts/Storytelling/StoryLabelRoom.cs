using Fungible.Movement;

namespace Fungible.Storytelling
{
    public class StoryLabelRoom : StoryLabelBaseObject
    {
        private void Awake()
        {
            GetComponent<Room>().EnterEvent += Show;
        }

        private void OnDestroy()
        {
            GetComponent<Room>().EnterEvent -= Show;
        }
    }
}