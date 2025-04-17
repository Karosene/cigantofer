using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    public GameObject spawnableAudioRef;
    public static GameObject spawnableAudio;

    private void Start(){
        spawnableAudio = spawnableAudioRef;
    }

    public static void SpawnAudio(AudioClip clip, float volume, Vector2 pos){
        Instantiate(spawnableAudio, pos, quaternion.identity).GetComponent<SpawnedAudioScript>().PlaySound(clip, volume);
    }
}
