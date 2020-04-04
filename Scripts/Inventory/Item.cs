using System.Collections;
using Fungible.Environment;
using UnityEngine;

namespace Fungible.Inventory
{
    [RequireComponent(typeof(ClickableObject))]
    public class Item : MonoBehaviour
    {
        public Sprite icon;

        private bool clickable;
        private AppearAnimationController animationController;

        private void Awake()
        {
            clickable = true;
            
            GetComponent<SpriteRenderer>().sortingOrder = GlobalConfig.SortOrderItem;
            animationController = GetComponent<AppearAnimationController>();
            GetComponent<ClickableObject>().ClickEvent += OnClick;
        }

        private void OnDestroy()
        {
            GetComponent<ClickableObject>().ClickEvent -= OnClick;
        }

        private void OnClick()
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
            if (!animationController)
                yield return null;
            else
                yield return animationController.SetInvisibleCoroutine();

            gameObject.SetActive(false);
        }
    }
}