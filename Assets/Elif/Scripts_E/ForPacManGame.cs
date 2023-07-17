using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForPacManGame : MonoBehaviour
{
     void OnEnable()
    {
        if(PlayerPrefs.HasKey("BuggedKey"))
        {
            Debug.Log("this exist");
        }
        else 
        {
            PlayerPrefs.SetFloat("PacmanKey", 1);
        }
    }
}
