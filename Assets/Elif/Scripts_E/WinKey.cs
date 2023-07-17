using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinKey : MonoBehaviour
{
    private int startKey = 0;
    private int keysTotal;


    public void Deneme()
    {
        if(!PlayerPrefs.HasKey("KeyOwned"))
        {
            PlayerPrefs.SetInt("KeyOwned",startKey);
        }

        else 
        {
            PlayerPrefs.SetInt("KeyOwned",keysTotal); 
        }
    }

    public void GainKey()
    {
        keysTotal =  keysTotal + 1;
    }

    public void GetPacmanKey()
    {
        
        //Check if player won key from this game
        if(PlayerPrefs.GetFloat("PacmanKey") > 4)
        {
            Debug.Log("You already got PacmanKey");
        }
        else
        {
            GainKey();
            PlayerPrefs.SetFloat("PacmanKey", 5);
            Debug.Log("You get PacmanKey");
        }
    }

    public void GetBuggedKey()
    {
        //Check if player won key from this game
        if(PlayerPrefs.GetFloat("BuggedKey") > 4)
        {
            Debug.Log("You already got BuggedKey");
        }
        else
        {
            GainKey();
            PlayerPrefs.SetFloat("BuggedKey", 5);
            Debug.Log("You get BuggedKey");
        }
    }

}
