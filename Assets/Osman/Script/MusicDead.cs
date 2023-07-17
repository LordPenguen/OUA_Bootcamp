using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDead : MonoBehaviour
{
    public AudioSource deadAudioSource;
    private bool hasCollidedWithGhost = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ghost") && !hasCollidedWithGhost)
        {
            // Pacman ve Ghost çarpıştığında yapılacak işlemler...
            if (deadAudioSource != null)
            {
                deadAudioSource.Play();
                hasCollidedWithGhost = true;
            }
        }
    }
}
