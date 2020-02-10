using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Fungible.UI
{
    public class ProxyControlsPanel : MonoBehaviour, IPointerDownHandler
    {
        public static ProxyControlsPanel Instance;
        
        private LayerMask _uiLayerMask;
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

        private void ProcessRaycastResults(IEnumerable<RaycastResult> raycastResults)
        {
            foreach (RaycastResult result in raycastResults)
            {
                GameObject touchedObject = result.gameObject;
                ClickableObject clickableObject = touchedObject.GetComponent<ClickableObject>();
                
                if (!IsLayerInMask(touchedObject.layer, _uiLayerMask))
                    clickableObject.OnClick();
            
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