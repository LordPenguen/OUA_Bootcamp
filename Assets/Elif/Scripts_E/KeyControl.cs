using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyControl : MonoBehaviour
{
    [SerializeField] private GameObject creditRawImage;

    private int enoughKey;


    private void OnTriggerEnter(Collider other)
    {
        Check();

        if(other.gameObject.tag == "Player" && enoughKey == 2){

            creditRawImage.gameObject.SetActive(true);

            Debug.Log("Door is open");

            PlayerPrefs.SetInt("KeyOwned", 0);
        }

        else
        {
            Debug.Log("You dont have enough keys");
        }
    }

    private void Check()
    {
        enoughKey = PlayerPrefs.GetInt("KeyOwned");
    }
}
