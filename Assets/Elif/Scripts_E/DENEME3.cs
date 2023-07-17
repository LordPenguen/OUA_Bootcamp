using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DENEME3 : MonoBehaviour
{
    public WinKey wk;
    
    private void OnTriggerEnter(Collider other) {
        
         if(other.gameObject.tag == "Player")
         { 
            //First Game Key
            wk.GainKey();

            Debug.Log("pacgamen");

        }
    }
}