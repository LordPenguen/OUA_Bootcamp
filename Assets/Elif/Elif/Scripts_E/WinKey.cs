using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinKey : MonoBehaviour
{
    [SerializeField] private int enoughPoints; //points we want them to gain in order to win the key
    private int points; //Initial Points or condition

    private bool keyOwned; //For not giving key twice

    //Gives key when conditions are done
    public void GainKey()
    {
        if( points >= enoughPoints)
        {
            if(keyOwned)
            {
                Debug.Log("You already win this key");
            } 

            else
            {
                Debug.Log("You win this key");
                GameVariables.keyCount++;
                keyOwned = true;
            }
        }
    }
}
