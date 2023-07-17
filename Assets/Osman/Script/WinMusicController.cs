using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMusicController : MonoBehaviour
{
    public AudioSource winAudioSource;

    public void PlayWinSound()
    {
        if (winAudioSource != null)
        {
            winAudioSource.Play();
        }
    }
}
