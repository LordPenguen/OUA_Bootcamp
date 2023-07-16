using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyControl : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && GameVariables.keyCount == 3){

            GameVariables.keyCount = 0;

            Debug.Log("Door is open");
            //opens the Dooor
            
        }
    }
}
