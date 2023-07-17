using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private AudioSource musicSource;

    private void Start()
    {
     
        musicSource = GetComponent<AudioSource>();
    }

   
    public void StopMusic()
    {
        musicSource.Pause();
    }

 
    public void PlayMusic()
    {
        musicSource.Play();
    }

   
    public void SetVolume(float volume)
    {
        
        volume = Mathf.Clamp01(volume);
        musicSource.volume = volume;
    }
}
