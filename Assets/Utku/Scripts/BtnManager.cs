using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BtnManager : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;
    public GameObject gameObject1;
    public GameObject gameObject2;
    public GameObject gameObject3;

    private void Start()
    {
        // Her bir butona tıklanma olaylarını atayın
        button1.onClick.AddListener(() => ButtonClicked(1));
        button2.onClick.AddListener(() => ButtonClicked(2));
        button3.onClick.AddListener(() => ButtonClicked(3));
    }

    private void ButtonClicked(int buttonIndex)
    {
        // İlgili butona tıklanırsa ilgili gameobjecti açın
        switch (buttonIndex)
        {
            case 1:
                gameObject1.SetActive(true);
                gameObject2.SetActive(false);
                gameObject3.SetActive(false);
                break;
            case 2:
                gameObject2.SetActive(true);
                gameObject1.SetActive(false);
                gameObject3.SetActive(false);
                break;
            case 3:
                gameObject3.SetActive(true);
                gameObject2.SetActive(false);
                gameObject1.SetActive(false);
                break;
        }
    }
}
