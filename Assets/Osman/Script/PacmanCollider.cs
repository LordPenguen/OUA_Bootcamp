using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PacmanCollider : MonoBehaviour
{
    private int score = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            GetComponent<PacmanPlayer>().Die();
        }
        if (other.CompareTag("Cherry"))
        {
            Destroy(other.gameObject);
            score++;
            print("score"+score);
        }
    }
}
