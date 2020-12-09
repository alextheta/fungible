using DG.Tweening;
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

        private static void EnableObject(GameObject entity)
        {
            if (!SaveController.LoadState)
            {
                entity.SetActive(true);
                var animationController = entity.GetComponent<SpriteAnimationController>();
                if (animationController)
                {
                    animationController.DisappearTween().Complete();
                    animationController.AppearTween();
                }
            }
            else if (!SaveController.IsPickedItem(entity))
            {
                entity.SetActive(true);
            }
        }

        private static void DisableObject(GameObject entity)
        {
            if (entity.activeInHierarchy && !SaveController.LoadState)
            {
                var animationController = entity.GetComponent<SpriteAnimationController>();
                if (animationController)
                {
                    entity.SetActive(true);
                    animationController.AppearTween().Complete();
                    animationController.DisappearTween().OnComplete(() => entity.SetActive(false));
                }
                else
                {
                    entity.SetActive(false);
                }
            }
            else if (!SaveController.IsPickedItem(entity))
            {
                entity.SetActive(false);
            }
        }
    }
}