﻿using System.Collections;
using UnityEngine;

namespace Fungible.Environment
{
    public class ObjectActivator : MonoBehaviour
    {
        [SerializeField] private GameObject[] objectsToActivate;
        [SerializeField] private GameObject[] objectsToDeactivate;

        public void Invoke()
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
            if (entity.activeInHierarchy)
                StartCoroutine(DisableObjectCoroutine(entity));
            else
                entity.SetActive(false);
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