using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HDU.Control;

namespace HDU.Movement
{
    public class objectMove : MonoBehaviour
    {
        private Vector3 mOffset;
        private float mZCoord;
        private GameObject selectedObject;

        public LayerMask layer;
        public bool isMoving = false;
        public objectMove other1;
        private void Update()
        {
            if (!other1.isMoving)
            {
                if (isMoving)
                {
                    transform.position = GetMouseAsWorldPoint() + mOffset;
                }
                raycastDragObject();
            }          
            if (Input.GetMouseButtonUp(0))
                isMoving = false;

        }
        void OnMouseDown()
        {
            
            mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            // Store offset = gameobject world pos - mouse world pos
            mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
        }
        private void OnMouseUp()
        {
            isMoving = false;
        }
        private Vector3 GetMouseAsWorldPoint()
        {
            // Pixel coordinates of mouse (x,y)

            Vector3 mousePoint = Input.mousePosition;

            // z coordinate of game object on screen

            mousePoint.z = mZCoord;
            // Convert it to world points

            return Camera.main.ScreenToWorldPoint(mousePoint);
        }

        void raycastDragObject()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Input.GetMouseButton(0))
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
                {
                    isMoving = true;
                    if (hit.collider != null)
                    {
                        selectedObject = hit.transform.gameObject;
                        if (selectedObject.GetComponent<wearableObject>() != null && selectedObject.GetComponent<wearableObject>().isDummy)
                        {
                            if (selectedObject.GetComponent<wearableObject>().rePosition)
                            {
                                selectedObject.GetComponent<wearableObject>().AfterRemovingCloth();
                                selectedObject.GetComponent<wearableObject>().rePosition = false;
                            }
                        }
                    }
                }
            }

        }

    }
}
