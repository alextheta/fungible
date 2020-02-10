using System.Collections.Generic;
using UnityEngine;

namespace Fungible.Movement
{
    public class Room : MonoBehaviour
    {
        public string backgroundResourceName;

        private Sprite backgroundSprite;

        public void OnEnter()
        {
            backgroundSprite = Resources.Load<Sprite>("Backgrounds/" + backgroundResourceName);
            Map.Instance.GetComponent<SpriteRenderer>().sprite = backgroundSprite;
        }

        public void OnLeave()
        {
            Resources.UnloadAsset(backgroundSprite);
        }
    }
}