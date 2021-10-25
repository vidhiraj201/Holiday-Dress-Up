using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDU.Control
{
    public class tutorialUI : MonoBehaviour
    {
        public GameObject fingerTapUITutorial;
        public float maxDelay = 2;
        public bool isPressed = false;
        private void Start()
        {
            fingerTapUITutorial.SetActive(false);
        }
        void Update()
        {
            if(!isPressed)
                UI();

            if (Input.GetMouseButtonDown(0))
            {
                isPressed = true;
                if (fingerTapUITutorial.activeSelf)
                    fingerTapUITutorial.SetActive(false);
            }
        }

        private void UI()
        {
            if (maxDelay >= 0)
            {
                maxDelay -= Time.deltaTime;
            }
            if (maxDelay <= 0)
            {
                fingerTapUITutorial.SetActive(true);
            }
            
        }
    }
}
