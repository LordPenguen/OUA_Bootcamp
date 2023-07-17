using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class KeyUI : MonoBehaviour
{
    public int ppKeyCount;

    public Image[] keys;

    public Sprite cyanKey;
    public Sprite blackKey;

   
    public void weirdUpdate()
    {
        ppKeyCount = PlayerPrefs.GetInt("KeyOwned");

        Debug.Log(ppKeyCount);

        foreach ( Image img in keys)
        {
            img.sprite = blackKey;
        }
        for (int i = 0; i < ppKeyCount; i++)
        {
            keys[i].sprite = cyanKey;
        }
    }
}
