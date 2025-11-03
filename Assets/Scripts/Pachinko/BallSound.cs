using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSound : MonoBehaviour
{

    public AudioClip bounceSfx;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bola menyentuh: " + collision.gameObject.name);
        if (bounceSfx != null && audioSource != null)
        {
            audioSource.PlayOneShot(bounceSfx);
        }
    }
}
