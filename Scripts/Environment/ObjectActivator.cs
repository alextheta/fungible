using System.Collections;
using Fungible.Inventory;
using UnityEngine;

namespace Fungible.Environment
{
    public class ObjectActivator : MonoBehaviour
    {
        public GameObject[] objectsToActivate;
        public GameObject[] objectsToDeactivate;

        private void Awake()
        {
            Item item = GetComponent<Item>();
            if (item)
                item.ClickEvent += OnClick;
            
            ItemPlaceHandler itemPlaceHandler = GetComponent<ItemPlaceHandler>();
            if (itemPlaceHandler)
                itemPlaceHandler.ClickEvent += OnClick;

            if (!item && !itemPlaceHandler)
            {
                ClickableObject clickableObject = GetComponent<ClickableObject>();
                if (clickableObject)
                    clickableObject.ClickEvent += OnClick;
            }
        }

        private void OnDestroy()
        {
            Item item = GetComponent<Item>();
            if (item)
                item.ClickEvent -= OnClick;
            
            ItemPlaceHandler itemPlaceHandler = GetComponent<ItemPlaceHandler>();
            if (itemPlaceHandler)
                itemPlaceHandler.ClickEvent -= OnClick;
            
            ClickableObject clickableObject = GetComponent<ClickableObject>();
            if (clickableObject)
                clickableObject.ClickEvent -= OnClick;
        }
        
        private void OnClick()
        {
            foreach (GameObject entity in objectsToActivate)
                EnableObject(entity);

            foreach (GameObject entity in objectsToDeactivate)
                DisableObject(entity);
        }

        private void EnableObject(GameObject entity)
        {
            StartCoroutine(EnableObjectCoroutine(entity));
        }

        private void DisableObject(GameObject entity)
        {
            StartCoroutine(DisableObjectCoroutine(entity));
        }

        private IEnumerator EnableObjectCoroutine(GameObject entity)
        {
            entity.SetActive(true);
            
            AppearAnimationController animationController = entity.GetComponent<AppearAnimationController>();
            if (animationController)
                yield return animationController.SetVisibleCoroutine();
        }
        
        private IEnumerator DisableObjectCoroutine(GameObject entity)
        {
            AppearAnimationController animationController = entity.GetComponent<AppearAnimationController>();
            if (animationController)
                yield return animationController.SetInvisibleCoroutine();
            
            entity.SetActive(false);
        }
    }
}
