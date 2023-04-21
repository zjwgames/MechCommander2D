using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudioClip(object[] args)
    {
        AudioClip clip = (AudioClip)args[0];
        Vector3 position = (Vector3)args[1];
        transform.position = position;
        audioSource.PlayOneShot(clip);
    }
}
