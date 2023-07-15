using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    string sceneName;

    public void LoadScene(string sceneName)
    {
        this.sceneName = sceneName;
        Invoke("DelayedLoad", 0.125f);
    }
    public void DelayedLoad ()
    {
        SceneManager.LoadScene(sceneName);
    }
}
