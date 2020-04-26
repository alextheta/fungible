﻿using System.Collections;
using Fungible.Environment;
using UnityEngine;

namespace Fungible.Inventory
{
    [RequireComponent(typeof(ClickableObject))]
    public class Item : MonoBehaviour
    {
        public Sprite icon;
        public delegate void ClickAction();
        public event ClickAction ClickEvent;

        private ClickableObject clickableObject;
        private AppearAnimationController animationController;

        private void Awake()
        {
            GetComponent<SpriteRenderer>().sortingOrder = GlobalConfig.SortOrderItem;
            animationController = GetComponent<AppearAnimationController>();
            clickableObject = GetComponent<ClickableObject>();
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

            ClickEvent?.Invoke();

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