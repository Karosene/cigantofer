using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HumanVision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            transform.parent.gameObject.GetComponent<Human>().PlayerEnter();
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            transform.parent.gameObject.GetComponent<Human>().PlayerExit();
        }
    }
}
