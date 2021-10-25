using System.Collections.Generic;
using UnityEngine;
using HDU.Control;

namespace HDU.Core
{
    public class scoring : MonoBehaviour
    {
        public List<GameObject> star = new List<GameObject>();

        public Charecter male;
        public Charecter female;

        public Animator anime;
        private bool oneStar, twoStar, threeStar;
        void Start()
        {
            anime = GetComponent<Animator>();
/*            for(int i = 0; i <= star.Count-1; i++)
            {
                star[i].SetActive(false);
            }*/
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void starCounting()
        {
            if (male.countCloth == 3 && female.countCloth == 3)
                threeStar = true;
            else if (male.countCloth == 2 && female.countCloth == 2)
                twoStar = true;
            else if (male.countCloth == 1 && female.countCloth == 1)
                oneStar = true;
            else
                oneStar = true;


            if (threeStar)
                anime.Play("3star");
            if (twoStar)
                anime.Play("2star");
            if (oneStar)
                anime.Play("1star");
        }
    }
}
