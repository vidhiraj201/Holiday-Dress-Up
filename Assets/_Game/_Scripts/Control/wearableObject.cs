using UnityEngine;
using HDU.Core;

namespace HDU.Control
{
    public class wearableObject : MonoBehaviour
    {

        public enum Type { Hoodie, Jacket, Tanktop, Shoe, Shorts, Pants, Tights, Tshirt, Underpants, Underwear }
        public Type wearingType;

        public enum PlacingPosition { Headgear, UpperTorso, LowerTorso, AP }
        public PlacingPosition placingPosition;

        public Charecter characterData;

        private SkinnedMeshRenderer SMR;

        public bool isDummy = false;


        public bool isFemale = false;
        public bool isMale = false;

        public Vector3 PuffOffset;

        // Start is called before the first frame update
        void Start()
        {
            SMR = transform.GetComponent<SkinnedMeshRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!isDummy)
                clothCheck();
        }


        void clothCheck()
        {
            
            if (characterData.WO == null)
                    return;

                if (characterData.WO.wearingType == this.wearingType)
                {
                    if (this.transform.name == characterData.WO.transform.name)
                    {
                        SMR.enabled = true;
                        characterData.Animation.SetTrigger("place");
                    if(characterData.WO.isFemale == isFemale || characterData.WO.isMale == isMale)
                    {
                        if (placingPosition == PlacingPosition.Headgear) { 
                            characterData.Headgear = true;
                            characterData.Head.enabled = false;
                        }
                        if (placingPosition == PlacingPosition.UpperTorso)
                        {
                            characterData.UpperTorso = true;
                            //characterData.Upper.enabled = false;

                        }
                        if (placingPosition == PlacingPosition.LowerTorso)
                        {
                            characterData.LowerTorso = true;
                            characterData.Lower.enabled = false;
                        }
                    }

                    if (characterData.WO.isFemale != isFemale || characterData.WO.isMale != isMale)
                    {
                        if (placingPosition == PlacingPosition.Headgear)
                            characterData.Headgear = false;
                        if (placingPosition == PlacingPosition.UpperTorso)
                            characterData.UpperTorso = false;
                        if (placingPosition == PlacingPosition.LowerTorso)
                            characterData.LowerTorso = false;
                    }

                    Destroy(Instantiate(characterData.SmokeFuff, transform.position + PuffOffset, Quaternion.identity), 2f);
                    Destroy(characterData.WO.gameObject);
                    characterData.WO.GetComponent<MeshRenderer>().enabled = false;
                    }
            }
        }
    }
}
