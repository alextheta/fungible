using System.Collections;
using UnityEngine;

namespace Fungible.Environment
{
    public class ObjectActivator : MonoBehaviour
    {
        [SerializeField] private GameObject[] objectsToActivate;
        [SerializeField] private GameObject[] objectsToDeactivate;

        public void Invoke()
        {
            foreach (var entity in objectsToActivate)
            {
                EnableObject(entity);
            }

            foreach (var entity in objectsToDeactivate)
            {
                DisableObject(entity);
            }
        }

        private void EnableObject(GameObject entity)
        {
            if (!SaveController.LoadState)
            {
                StartCoroutine(EnableObjectCoroutine(entity));
            }
            else if (!SaveController.IsPickedItem(entity))
            {
                entity.SetActive(true);
            }
        }

        private void DisableObject(GameObject entity)
        {
            if (entity.activeInHierarchy && !SaveController.LoadState)
            {
                StartCoroutine(DisableObjectCoroutine(entity));
            }
            else if (!SaveController.IsPickedItem(entity))
            {
                entity.SetActive(false);
            }
        }

        private static IEnumerator EnableObjectCoroutine(GameObject entity)
        {
            entity.SetActive(true);

            var animationController = entity.GetComponent<AppearAnimationController>();
            if (animationController)
            {
                yield return animationController.SetVisibleCoroutine();
            }
        }

        private static IEnumerator DisableObjectCoroutine(GameObject entity)
        {
            var animationController = entity.GetComponent<AppearAnimationController>();
            if (animationController)
            {
                yield return animationController.SetInvisibleCoroutine();
            }

            entity.SetActive(false);
        }
    }
}