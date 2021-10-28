using UnityEngine;
using HDU.Core;
using HDU.Movement;

namespace HDU.Control
{
    public class wearableObject : MonoBehaviour
    {

        /*public enum Type { Hoodie, Jacket, Tanktop, Shoe, Shorts, Pants, Tights, Tshirt, Underpants, Underwear }
        public Type wearingType;*/

        public enum PlacingPosition { Headgear, UpperTorso, LowerTorso, AP }
        public PlacingPosition placingPosition;

        public Charecter characterData;
        public wearableObject WO;
        private SkinnedMeshRenderer SMR;
        private MeshRenderer MR;

        public bool isDummy = false;
        public bool rePosition = false;
        public bool isClothPlaced = false;

        

        public bool isFemale = false;
        public bool isMale = false;

        public Vector3 PlacingPostion;
        public Vector3 hintPlace;
        public Vector3 PuffOffset;

        private Vector3 initPosition;
        private float countDown = 0;
        // Start is called before the first frame update
        void Start()
        {
            countDown = 0;

            if (!isDummy)
                SMR = transform.GetComponent<SkinnedMeshRenderer>();

            if (isDummy)
                MR = transform.GetComponent<MeshRenderer>();

            initPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (countDown >= 0)
            {
                countDown -= Time.deltaTime;
                clothCheck();

            }


            if (WO != null)
                

            if (Input.GetMouseButtonUp(0))
            {
                    WO.characterData.checkFM();
                    clothCheck();
            }

            if (isDummy && !transform.GetComponent<objectMove>().isMoving && WO == null && countDown<=0)
            {
                ResetPosition();
            }
        }


        public void clothCheck()
        {
            if (WO == null)
                return;

            if (!WO.isClothPlaced && WO.placingPosition == this.placingPosition)
            {
                if (this.transform.name == WO.transform.name)
                {
                    WO.SMR.enabled = true;
                    WO.isClothPlaced = true;
                    WO.characterData.Animation.SetTrigger("place");

                    if (isFemale)
                        WO.characterData.FemaleCloth = true;
                    if (isMale)
                        WO.characterData.MaleCloth = true;


                    if (WO.isFemale == isFemale || WO.isMale == isMale)
                    {
                        if (placingPosition == PlacingPosition.Headgear)
                        {
                            WO.characterData.Headgear = true;
                        }
                        if (placingPosition == PlacingPosition.UpperTorso)
                        {
                            WO.characterData.UpperTorso = true;

                        }
                        if (placingPosition == PlacingPosition.LowerTorso)
                        {
                            WO.characterData.LowerTorso = true;
                        }
                    }

                    if (WO.isFemale != isFemale || WO.isMale != isMale)
                    {
                        if (placingPosition == PlacingPosition.Headgear)
                            WO.characterData.Headgear = false;
                        if (placingPosition == PlacingPosition.UpperTorso)
                            WO.characterData.UpperTorso = false;
                        if (placingPosition == PlacingPosition.LowerTorso)
                            WO.characterData.LowerTorso = false;
                    }


                    FindObjectOfType<GameManager>().source.PlayOneShot(FindObjectOfType<GameManager>().ClothClip, 1);
                    Destroy(Instantiate(WO.characterData.SmokeFuff, transform.position + PuffOffset, Quaternion.identity), 2f);
                    transform.GetComponent<Collider>().isTrigger = false;
                    MR.enabled = false;
                    rePosition = true;
                    
                }
            }
        }

        public void clothRemoved()
        {
            if (WO == null)
                return;

            if (WO.isClothPlaced && WO.placingPosition == this.placingPosition)
            {
                if (this.transform.name == WO.transform.name)
                {
                    WO.SMR.enabled = false;
                    WO.isClothPlaced = false ;
                    /*WO.characterData.Animation.SetTrigger("place");*/
                    if (WO.isFemale == isFemale || WO.isMale == isMale)
                    {
                        if (placingPosition == PlacingPosition.Headgear)
                        {
                            WO.characterData.Headgear = false;
                        }
                        if (placingPosition == PlacingPosition.UpperTorso)
                        {
                            WO.characterData.UpperTorso = false;

                        }
                        if (placingPosition == PlacingPosition.LowerTorso)
                        {
                            WO.characterData.LowerTorso = false;
                        }
                    }

                    if (WO.isFemale != isFemale || WO.isMale != isMale)
                    {
                        if (placingPosition == PlacingPosition.Headgear)
                            WO.characterData.Headgear = true;
                        if (placingPosition == PlacingPosition.UpperTorso)
                            WO.characterData.UpperTorso = true;
                        if (placingPosition == PlacingPosition.LowerTorso)
                            WO.characterData.LowerTorso = true;
                    }
                }
            }
        }

        public void AfterRemovingCloth()
        {
            clothRemoved();
            //WO.characterData.resetAnimation();
            MR.enabled = true;
            transform.GetComponent<Collider>().isTrigger = true;
            FindObjectOfType<GameManager>().source.PlayOneShot(FindObjectOfType<GameManager>().ClothClip, 1);
        }

        public void ResetPosition()
        {            
            transform.position = initPosition;
            
        }

        private void OnTriggerStay(Collider other)
        {
            if (WO == null)
                WO = other.GetComponent<wearableObject>();
        }

        private void OnTriggerExit(Collider other)
        {
            if (transform.GetComponent<objectMove>().isMoving)
                WO = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(WO==null)
                WO = other.GetComponent<wearableObject>();

            /* if (!transform.GetComponent<objectMove>().isMoving && other.gameObject.CompareTag("Rack")) ;
                 ResetPosition();*/
        }


        
        public void HintButton()
        {
            countDown = 1;
            transform.localPosition = hintPlace;
            clothCheck();
        }
    }
}
