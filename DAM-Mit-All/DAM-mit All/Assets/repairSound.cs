using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repairSound : MonoBehaviour
{
    public AudioClip[] clips = new AudioClip[3];

    private void Start()
    {

    }
    public void PlayAudio()
    {
        GetComponent<AudioSource>().PlayOneShot(clips[Random.Range(0, 3)]);
    }

}
