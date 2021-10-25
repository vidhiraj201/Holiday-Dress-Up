using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDU.Movement
{
    public class objectMove : MonoBehaviour
    {
        private Vector3 mOffset;
        private float mZCoord;

        public bool isMoving = false;
        private void Update()
        {
            if (isMoving)
            {
                transform.position = GetMouseAsWorldPoint() + mOffset;
            }
        }
        void OnMouseDown()
        {
            isMoving = true;
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

        /*void raycastDragObject()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, draggableMask))
                {
                    if (hit.collider != null)
                    {
                        Debug.Log("Hit");
                        selectedObject = hit.collider.gameObject;
                        isMoving = true;
                    }
                }
            }

            if (isMoving)
            {
                Vector3 pos = mousePos();

                selectedObject.transform.position = pos;
            }
            if (Input.GetMouseButtonUp(0))
                isMoving = false;
        }

        Vector3 mousePos()
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Infinity));
        }*/
    }
}
