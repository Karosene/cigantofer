using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomSprite : MonoBehaviour
{
    public Sprite[] sprites;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360)); 
    }
}   