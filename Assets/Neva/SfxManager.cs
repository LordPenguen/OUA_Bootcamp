using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SfxManager : MonoBehaviour
{
    public AudioSource btnSfx;
    private Button btn;
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(PlaySound);
        btnSfx.playOnAwake = false;
    }

    private void PlaySound()
    {
        btnSfx.Play();
    }
}
