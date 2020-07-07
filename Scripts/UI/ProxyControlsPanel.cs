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

        private LayerMask _uiLayerMask;
        private bool _controlsAllowed = true;

        public void EnableControls()
        {
            _controlsAllowed = true;
        }

        public void DisableControls()
        {
            _controlsAllowed = false;
        }

        public bool ControlsAllowed()
        {
            return _controlsAllowed;
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
            raycastResults.RemoveAll((result => IsLayerInMask(result.gameObject.layer, _uiLayerMask)));

            try
            {
                raycastResults.Sort((a, b) =>
                {
                    /* Sort in reverse order */
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
                }
            }
        }

        private void Awake()
        {
            Instance = this;
            _uiLayerMask = LayerMask.NameToLayer("UI");
        }

        private static bool IsLayerInMask(int layer, LayerMask layerMask)
        {
            return (layerMask & layer) == layerMask;
        }
    }
}