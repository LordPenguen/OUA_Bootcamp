using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deneme : MonoBehaviour
{
    public WinKey wk;
    
    private void OnTriggerStay(Collider other) {

        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
         { 

            //PAcman GAMe
            SceneManager.LoadScene("PacmanScene"); 
            wk.GainKey();

            Debug.Log("pacgamen");

        }
    }
}
