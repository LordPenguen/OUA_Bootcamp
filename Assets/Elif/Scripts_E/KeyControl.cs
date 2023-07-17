using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyControl : MonoBehaviour
{
    [SerializeField] private GameObject creditRawImage;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && GameVariables.keyCount == 3){

            GameVariables.keyCount = 0;

            creditRawImage.gameObject.SetActive(true);

            Debug.Log("Door is open");
            
        }
    }
}
