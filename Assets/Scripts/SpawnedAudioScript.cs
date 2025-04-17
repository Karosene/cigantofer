using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedAudioScript : MonoBehaviour
{
    public void PlaySound(AudioClip clip, float volume){
        AudioSource a = GetComponent<AudioSource>();
        a.clip = clip;
        a.volume = volume;

        a.Play();

        Destroy(gameObject, clip.length + 1f);
    }
}
