using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDU.Control
{
    public class CharacterAnimationControll : MonoBehaviour
    {
        public void resetWalk()
        {
            if (transform.GetChild(0).GetComponent<Charecter>().walkahead)
            {
                transform.GetChild(0).GetComponent<Charecter>().walkahead = false;
            }

            transform.GetComponent<Animator>().SetBool("in", false);
            transform.GetComponent<Animator>().SetBool("out", false);
        }
    }
}
