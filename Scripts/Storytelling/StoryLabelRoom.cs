using Fungible.Movement;

namespace Fungible.Storytelling
{
    public class StoryLabelRoom : StoryLabelObject
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