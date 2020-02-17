using System.Collections;
using Fungible.Environment;
using Fungible.UI;
using UnityEngine;

namespace Fungible.Inventory
{
    public class Item : ClickableObject
    {
        public Sprite icon;

        private bool clickable;
        private AppearAnimationController animationController;

        private void Awake()
        {
            clickable = true;
            
            GetComponent<SpriteRenderer>().sortingOrder = GlobalConfig.SortOrderItem;
            animationController = GetComponent<AppearAnimationController>();
        }

        public override void OnClick()
        {
            if (!clickable || !InventoryController.Instance.AddItem(this))
                return;

            clickable = false;

            ObjectActivator activator = GetComponent<ObjectActivator>();
            if (activator != null)
                activator.Invoke();
            
            SelfDisable();
        }

        private void SelfDisable()
        {
            StartCoroutine(SelfDisableCoroutine());
        }

        private IEnumerator SelfDisableCoroutine()
        {
            if (ReferenceEquals(animationController, null))
                yield return null;
            else
                yield return animationController.SetInvisibleCoroutine();

            gameObject.SetActive(false);
        }
    }
}