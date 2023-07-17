using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deneme2 : MonoBehaviour
{
    public WinKey wk;
    
    private void OnTriggerStay(Collider other) {
        
         if(other.gameObject.tag == "Player" && Input.GetKeyDown("E"))
         { 

            //buggedGame
            SceneManager.LoadScene(5); 
            wk.GainKey();

            Debug.Log("buggedgame");

        }
    }
}
