using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HDU.Core;

namespace HDU.Control
{
    public class Charecter : MonoBehaviour
    {

        [Header("Placing Positons")]
        public bool Headgear;
        public bool UpperTorso;
        public bool LowerTorso;

        public GameObject SmokeFuff;

        public Charecter otherCharacter;

        private GameManager gm;
        public bool walkahead = false;



        public bool MaleCloth;
        public bool FemaleCloth;
        [HideInInspector]public Animator Animation;
        [HideInInspector]public int PlaceingNumber;
        [HideInInspector]public int countCloth;

        void Start()
        {
            Animation = GetComponent<Animator>();
            gm = FindObjectOfType<GameManager>();
        }

        void Update()
        {
            Submit();
            clothCount();
        }

        void clothCount()
        {
            if (Headgear && UpperTorso && LowerTorso)
                countCloth = 3;

           else if (!Headgear && UpperTorso && LowerTorso)
                countCloth = 2;

           else if (Headgear && !UpperTorso && LowerTorso)
                countCloth = 2;

           else if (Headgear && UpperTorso && !LowerTorso)
                countCloth = 2;

            else
                countCloth = 1;

        }

        void Submit()
        {
            if (gm.Submited)
            {
                if (PlaceingNumber == 0)
                {
                    Animation.SetTrigger("frust");
                }
                if (PlaceingNumber == 1)
                {
                    Animation.SetTrigger("ch");
                }
            }
            
        }
        public void checkFM()// check Cloth is male or female
        {

            if (!walkahead)
            {
                Animation.Play("Walking");
                if (otherCharacter.transform.parent.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("IN"))
                {
                    otherCharacter.transform.parent.GetComponent<Animator>().SetBool("out", true);
                    otherCharacter.Animation.Play("WalkBack");
                    otherCharacter.transform.parent.GetComponent<Animator>().SetBool("in", false);
                }
                if (!transform.parent.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("IN"))
                    transform.parent.GetComponent<Animator>().SetBool("in", true);
                walkahead = true;
            }
        } 
    }
}


