using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using HDU.Control;

namespace HDU.Movement
{
    public class clothMovement : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
    {

        [SerializeField] private Canvas canvas;
        private RectTransform rectTransform;

        public bool onCharacter = false;
        public LayerMask LayerToSelect;
        private Vector3 initPos;
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            initPos = rectTransform.localPosition;
        }
        public void raycastData()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerToSelect))
            {
                if (hit.collider != null)
                {

                    GetComponent<wearableObject>().WO = hit.collider.GetComponent<wearableObject>();
                    onCharacter = true;
                }
            }
            else
            {
                onCharacter = false;
                GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, 1);
                GetComponent<wearableObject>().AfterRemovingCloth();
            }
        }

        #region drag
        public void OnBeginDrag(PointerEventData eventData)
        {

        }
        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            raycastData();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (GetComponent<wearableObject>().WO == null)
                rectTransform.localPosition = initPos;

            if (GetComponent<wearableObject>().WO != null)
            {
                GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, 0);
                GetComponent<wearableObject>().clothCheck();
                if(!GetComponent<wearableObject>().WO.isDummy)
                    GetComponent<wearableObject>().WO.characterData.checkFM();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {

        }

        public void OnDrop(PointerEventData eventData)
        {

        }

        #endregion
    }

}