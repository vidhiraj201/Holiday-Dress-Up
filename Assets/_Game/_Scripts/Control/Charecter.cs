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

        
        [Header(" ")]
        public Collider Head;
        public Collider Upper;
        public Collider Lower;

        public GameObject SmokeFuff;

        public Charecter otherCharacter;

        private GameManager gm;
        private bool walkahead = false;



        [HideInInspector]public bool MaleCloth;
        [HideInInspector]public bool FemaleCloth;
        [HideInInspector]public wearableObject WO;
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
            if(WO != null)
            {
                if (WO.isMale)
                    MaleCloth = true;
                if (WO.isFemale)
                    FemaleCloth = true;
            }
            
        }


        private void OnTriggerStay(Collider other)
        {
            if (Input.GetMouseButtonUp(0))
            {
                checkFM(other);
            }
        }
        void checkFM(Collider other)// check Cloth is male or female
        {

            WO = other.gameObject.GetComponent<wearableObject>();
            if (!walkahead)
            {
                Animation.Play("Walking");
                if (otherCharacter.transform.parent.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("IN"))
                {
                    otherCharacter.transform.parent.GetComponent<Animator>().SetBool("out", true);
                    otherCharacter.transform.parent.GetComponent<Animator>().SetBool("in", false);
                }
                if (!transform.parent.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("IN"))
                    transform.parent.GetComponent<Animator>().SetBool("in", true);
                walkahead = true;
            }
        }
    }
}


