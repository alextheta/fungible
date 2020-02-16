using UnityEngine;

namespace Fungible.Movement
{
    public class Room : MonoBehaviour
    {
        public string backgroundResourceName;

        private Sprite backgroundSprite;

        public void OnEnter()
        {
            backgroundSprite = LoadSprite();
            Map.Instance.GetSpriteRenderer().sprite = backgroundSprite;
        }

        public void OnLeave()
        {
            Resources.UnloadAsset(backgroundSprite);
        }

        public Sprite LoadSprite()
        {
            return Resources.Load<Sprite>("Backgrounds/" + backgroundResourceName);
        }
    }
}