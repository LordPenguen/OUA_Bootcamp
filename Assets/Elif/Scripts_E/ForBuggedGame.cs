using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForBuggedGame : MonoBehaviour
{
    void OnEnable()
    {
        if(PlayerPrefs.HasKey("BuggedKey"))
        {
            Debug.Log("this exist");
        }
        else 
        {
            PlayerPrefs.SetFloat("BuggedKey", 1);
        }
    }

    
}
