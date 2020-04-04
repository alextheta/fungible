using System.Collections;
using Fungible.Environment;
using UnityEngine;

namespace Fungible.Inventory
{
    [RequireComponent(typeof(ClickableObject))]
    public class Item : MonoBehaviour
    {
        public Sprite icon;

        private ClickableObject clickableObject;
        private AppearAnimationController animationController;

        private void Awake()
        {
            GetComponent<SpriteRenderer>().sortingOrder = GlobalConfig.SortOrderItem;
            clickableObject = GetComponent<ClickableObject>();
            animationController = GetComponent<AppearAnimationController>();
            clickableObject.ClickEvent += OnClick;
        }

        private void OnDestroy()
        {
            clickableObject.ClickEvent -= OnClick;
        }

        private void OnClick()
        {
            if (!clickableObject.clickable || !InventoryController.Instance.AddItem(this))
                return;

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
            if (!animationController)
                yield return null;
            else
                yield return animationController.SetInvisibleCoroutine();

            gameObject.SetActive(false);
        }
    }
}