using Fungible.Inventory;
using UnityEngine;

namespace Fungible.Environment
{
    [RequireComponent(typeof(ItemPlaceHandler))]
    public class ObjectActivator : MonoBehaviour
    {
        public GameObject[] objectsToActivate;
        public GameObject[] objectsToDeactivate;

        public void Invoke()
        {
            foreach (GameObject entity in objectsToActivate)
                entity.SetActive(true);
            
            foreach (GameObject entity in objectsToDeactivate)
                entity.SetActive(false);
        }
    }
}
