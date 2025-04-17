using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineScript : MonoBehaviour
{
    void Update()
    {
        if(transform.parent != null){
            SpriteRenderer weaponSpriteR = transform.parent.parent.GetComponentInChildren<SpriteRenderer>();

            GetComponent<SpriteRenderer>().sortingLayerID = weaponSpriteR.sortingLayerID;
            GetComponent<SpriteRenderer>().sortingOrder = weaponSpriteR.sortingOrder -1;
        }
        else{
            GetComponent<SpriteRenderer>().sortingLayerName = "GroundClutter";
            GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
    }
}
