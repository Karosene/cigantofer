using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Damagable : MonoBehaviour
{
    public float health;
    public GameObject effect;
    public AudioCue hitSound;
    protected Rigidbody2D rb;
    public GameObject deadBody;
    public GameObject deadHead;
    public GameObject deadTorso;
    public GameObject deadOthers;
    private Vector2 lastRotation;
    private bool isHighCalliber;

    void Start()
    {

    }

    public void Damage(float damage, Vector2 pos, Vector2 rot, bool isHighCalliberr){
        GameObject g = Instantiate(effect, pos, Quaternion.identity);
        g.transform.up = rot;

        lastRotation = rot;
        this.isHighCalliber = isHighCalliberr;
        health -= damage;
        Debug.Log(health);

        if(health <= 0){
            Destroy(gameObject);
        }
    }

    void Update()
    {

    }

    private void OnDestroy() {
        if(deadBody == null || deadHead == null|| deadOthers == null|| deadTorso == null) return;

        if(!isHighCalliber) {
            GameObject dB = Instantiate(deadBody, transform.position, Quaternion.identity);
            dB.GetComponent<Rigidbody2D>().AddForce(lastRotation + new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * Random.Range(-150, -400), ForceMode2D.Impulse);
        } else{
            GameObject dH = Instantiate(deadHead, transform.position, Quaternion.identity);
            dH.GetComponent<Rigidbody2D>().AddForce(lastRotation + new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * Random.Range(-150, -400), ForceMode2D.Impulse);

            GameObject dT = Instantiate(deadTorso, transform.position, Quaternion.identity);
            Instantiate(deadTorso, transform.position, Quaternion.identity);
            dT.GetComponent<Rigidbody2D>().AddForce(lastRotation + new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * Random.Range(-150, -400), ForceMode2D.Impulse);

            GameObject dO = Instantiate(deadOthers, transform.position, Quaternion.identity);
            Instantiate(deadOthers, transform.position, Quaternion.identity);
            dO.GetComponent<Rigidbody2D>().AddForce(lastRotation + new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * Random.Range(-150, -400), ForceMode2D.Impulse);
        }
    }
}