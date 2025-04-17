using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunScript : MonoBehaviour
{
     public Transform target; // Assign this to the enemy or target in the Inspector
    private BulletScript bullet; 

    void Start(){
        bullet = GetComponent<BulletScript>();
    }
    void Update()
    {
        if(target != null) {
            float distanceFromTarget = Vector2.Distance(transform.position, target.position);;

            if(distanceFromTarget > 0 && distanceFromTarget <= 20) {
                GetComponent<BulletScript>().damage = 35;
            }
            if(distanceFromTarget > 20 && distanceFromTarget <= 40) {
                GetComponent<BulletScript>().damage = 25;
            }
            if(distanceFromTarget > 40 && distanceFromTarget <= 70) {
                GetComponent<BulletScript>().damage = 15;
            }
            else{
                GetComponent<BulletScript>().damage = 10;
            }
        }
    }
}
