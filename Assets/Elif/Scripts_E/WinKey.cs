using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class WinKey : MonoBehaviour
{
    private int startKey = 0;
    private int keysTotal;
    public KeyUI keyColors;

    public void OnEnable()
    {
        if(PlayerPrefs.HasKey("KeyOwned"))
        {
            PlayerPrefs.SetInt("KeyOwned",keysTotal);
        }

        else 
        {
            PlayerPrefs.SetInt("KeyOwned",startKey); 
        }
    }

    public void GainKey()
    {
        keysTotal = keysTotal + 1;
        PlayerPrefs.SetInt("KeyOwned",keysTotal);
        keyColors.weirdUpdate();
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
