using UnityEngine;

namespace Fungible.Movement
{
    public class Room : MonoBehaviour
    {
        public string backgroundResourceName;
        public Sprite backgroundSprite;

        public void OnEnter()
        {
            backgroundSprite = Resources.Load<Sprite>("Backgrounds/" + backgroundResourceName);
        }

        public void OnLeave()
        {
            Resources.UnloadAsset(backgroundSprite);
        }
    }
}