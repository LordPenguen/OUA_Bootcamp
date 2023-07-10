using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fullScreen : MonoBehaviour

{
    void Start()
    {
        // Toggle fullscreen
        Screen.fullScreen = !Screen.fullScreen;
    }
}

