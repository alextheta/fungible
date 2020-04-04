using System.Collections;
using Fungible.Storytelling;
using Fungible.UI;
using UnityEngine;

namespace Fungible.Environment
{
    public class ObjectActivator : MonoBehaviour
    {
        public GameObject[] objectsToActivate;
        public GameObject[] objectsToDeactivate;

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
