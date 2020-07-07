using System.Collections.Generic;
using System.Linq;
using Fungible.Environment;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

namespace Fungible.UI
{
    public class ProxyControlsPanel : MonoBehaviour, IPointerDownHandler
    {
        public static ProxyControlsPanel Instance;

        private LayerMask uiLayerMask;
        private Camera mainCamera;
        private bool controlsAllowed = true;

        public void EnableControls()
        {
            controlsAllowed = true;
        }

        public void DisableControls()
        {
            controlsAllowed = false;
        }

        public bool ControlsAllowed()
        {
            return controlsAllowed;
        }

        public void OnPointerDown(PointerEventData pointerEventData)
        {
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);

            if (raycastResults.Count <= 0)
                return;

            ProcessRaycastResults(raycastResults);
        }

        private void ProcessRaycastResults(List<RaycastResult> raycastResults)
        {
            raycastResults.RemoveAll((result => IsLayerInMask(result.gameObject.layer, uiLayerMask)));

            try
            {
                raycastResults.Sort((a, b) =>
                {
                    // sort in reverse order
                    Debug.Log("A [" + a.gameObject + "] B [" + b.gameObject + "]");
                    int aSortingOrder = a.gameObject.GetComponent<SortingGroup>().sortingOrder;
                    int bSortingOrder = b.gameObject.GetComponent<SortingGroup>().sortingOrder;

                    return bSortingOrder - aSortingOrder;
                });
            }
            catch (System.Exception ex)
            {
                Debug.LogException(ex);
            }

            if (raycastResults.Count > 0)
            {
                int topSortingOrder = raycastResults.First().gameObject.GetComponent<SortingGroup>().sortingOrder;
                foreach (RaycastResult raycastResult in raycastResults)
                {
                    int currentObjectSortingOrder = raycastResult.gameObject.GetComponent<SortingGroup>().sortingOrder;
                    if (currentObjectSortingOrder != topSortingOrder)
                        break;

                    ClickEventSender eventSender = raycastResult.gameObject.GetComponent<ClickEventSender>();
                    eventSender.Invoke();
                    Debug.Log("Proxy click on " + eventSender);
                }
            }
        }

        private void Awake()
        {
            Instance = this;
            uiLayerMask = LayerMask.NameToLayer("UI");
            mainCamera = Camera.main;
        }

        private static bool IsLayerInMask(int layer, LayerMask layerMask)
        {
            return (layerMask & layer) == layerMask;
        }
    }
}