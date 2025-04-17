using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasingScript : MonoBehaviour
{
    public float startY;
    public AudioCue casingSound; //casing sound
    private float _minSpeedForSound = 1.5f; //minimalna rychlost aby sa mi sputil zvuk
    private bool _soundPlayed = false; // ci mi zvuk uz hra


    private void Update()
    {
        if(transform.position.y > startY) return;

        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Rigidbody2D>().drag = 8;

        if(GetComponent<Rigidbody2D>().velocity.magnitude < _minSpeedForSound && !_soundPlayed){ //tu sa mi spusti zvuk iba ak rychlost nabojnice alebo zasobnika je mensia ako prednastavena rychlost
            PlayCasingSound();
            _soundPlayed = true;
        }
    }

    private void PlayCasingSound(){ // funkcia na sound effect
         AudioClip casingClip = casingSound.GetSound();
         AudioManagerScript.SpawnAudio(casingClip, casingSound.volume, transform.position);
    }
}
