using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    private AudioSource audioSource;

    private GameController gameController;


    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        audioSource = GetComponent<AudioSource>();
        audioSource.mute = gameController.Muted;
    }

    void Update()
    {
        audioSource.mute = gameController.Muted;
    }


    public void PlayOneShot(AudioClip clip)
    {
        if (clip)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
