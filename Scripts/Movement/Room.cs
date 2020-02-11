using UnityEngine;

namespace Fungible.Movement
{
    public class Room : MonoBehaviour
    {
        public string backgroundResourceName;

        private Sprite backgroundSprite;
        private SpriteRenderer mapSpriteRenderer;

        private void Start()
        {
            mapSpriteRenderer = Map.Instance.GetComponent<SpriteRenderer>();
        }

        public void OnEnter()
        {
            backgroundSprite = LoadSprite();
            Map.Instance.GetComponent<SpriteRenderer>().sprite = backgroundSprite;
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