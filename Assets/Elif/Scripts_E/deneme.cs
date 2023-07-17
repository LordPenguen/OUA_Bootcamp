using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deneme : MonoBehaviour
{
    public WinKey wk;
    
    private void OnTriggerEnter(Collider other) {
        
         if(other.gameObject.tag == "Player")
         {

            wk.GainKey();

            Debug.Log("Denemeanahtarı");

        }
    }
}
