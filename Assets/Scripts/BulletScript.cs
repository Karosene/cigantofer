using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.PackageManager;



// using System.Drawing;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float shootForce;
    public bool isHighCalliber;
    public float damage;

    public void Setup()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.right * (shootForce * Random.Range(0.9f, 1.1f)));

        Destroy(gameObject, 2);
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.GetComponent<Damagable>())
        {
            other.gameObject.GetComponent<Damagable>().Damage(damage, transform.position, -transform.right, isHighCalliber);
        }

        Destroy(gameObject);

        
    }
}