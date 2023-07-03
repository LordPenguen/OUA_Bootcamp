using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryGameManager : MonoBehaviour
{
    public float delay;
    //public Sprite bg;
    public AudioSource TypeSfx;

    [Multiline]
    public string txt;

    TextMeshProUGUI thisText;
    bool isTyping;
    bool isFinishedTyping;

    private void Start()
    {
        thisText = GetComponent<TextMeshProUGUI>();
        isTyping = false;
        isFinishedTyping = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isTyping && !isFinishedTyping)
        {
            StartCoroutine(TypeWrite());
        }
    }

    IEnumerator TypeWrite()
    {
        isTyping = true;
        thisText.text = "";

        foreach (char i in txt)
        {
            thisText.text += i.ToString();
            TypeSfx.pitch = Random.Range(0.8f, 1.2f);
            TypeSfx.Play();

            if (i.ToString() == ".")
            {
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                yield return new WaitForSeconds(delay);
            }
        }

        isTyping = false;
        isFinishedTyping = true;
    }
}


