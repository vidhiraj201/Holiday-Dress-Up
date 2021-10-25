using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using HDU.Control;

namespace HDU.Core
{
    public class GameManager : MonoBehaviour
    {
        [Header("Male Placings Places")]
        public bool MHeadgear;
        public bool MUpperTorso;
        public bool MLowerTorso;

        [Header("Female Placings Places")]
        public bool FHeadgear;
        public bool FUpperTorso;
        public bool FLowerTorso;

        [Header("")]
        public GameObject Confetti;
        public GameObject submitButton;
        public GameObject GameOver;
        public GameObject male, female;

        public bool Submited;

        private void Start()
        {
            GameOver.SetActive(false);
        }

        private void Update()
        {
            maleCloth();
            femaleCloth();

            if(Submited)
                StartCoroutine(gameOver());
        }
        public void submit()
        {
            if (male.GetComponent<Charecter>().PlaceingNumber == 1 && female.GetComponent<Charecter>().PlaceingNumber == 1 )
            {
                Submited = true;
                Confetti.SetActive(true);
                
            }
            if (male.GetComponent<Charecter>().PlaceingNumber == 0 || female.GetComponent<Charecter>().PlaceingNumber == 0)
            {
                Submited = true;
            }
            Destroy(submitButton);
        }

        void maleCloth()
        {
            if (MHeadgear == male.GetComponent<Charecter>().Headgear && MUpperTorso == male.GetComponent<Charecter>().UpperTorso && MLowerTorso == male.GetComponent<Charecter>().LowerTorso)
            {
                male.GetComponent<Charecter>().PlaceingNumber = 1;
            }
            if (MHeadgear != male.GetComponent<Charecter>().Headgear || MUpperTorso != male.GetComponent<Charecter>().UpperTorso || MLowerTorso != male.GetComponent<Charecter>().LowerTorso)
            {
                male.GetComponent<Charecter>().PlaceingNumber = 0;
            }
        }

        void femaleCloth()
        {


            if (FHeadgear == female.GetComponent<Charecter>().Headgear && FUpperTorso == female.GetComponent<Charecter>().UpperTorso && FLowerTorso == female.GetComponent<Charecter>().LowerTorso)
            {
                female.GetComponent<Charecter>().PlaceingNumber = 1;
            }
            if (FHeadgear != female.GetComponent<Charecter>().Headgear || FUpperTorso != female.GetComponent<Charecter>().UpperTorso || FLowerTorso != female.GetComponent<Charecter>().LowerTorso)
            {
                female.GetComponent<Charecter>().PlaceingNumber = 0;
            }
        }




        IEnumerator gameOver()
        {
            yield return new WaitForSeconds(1f);
            GameOver.SetActive(true);
            FindObjectOfType<scoring>().starCounting();
        }

        public void nextLevel()
        {
            SceneManager.LoadScene(0);
        }
    }

}
