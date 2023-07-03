using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class TextManager : MonoBehaviour
{
    public List<TextMeshProUGUI> textElements;
    private int currentIndex;
    private bool isFirstClick = true;
    private bool isSecondClick = false;
    public GameObject currentScene;
    public GameObject nextScene;

    private void Start()
    {
        currentIndex = 0;

        // İlk öğeyi aktif hale getiriyoruz, diğerlerini ise pasif hale getiriyoruz
        for (int i = 0; i < textElements.Count; i++)
        {
            if (i == currentIndex)
            {
                textElements[i].gameObject.SetActive(true);
            }
            else
            {
                textElements[i].gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isFirstClick)
            {
                isFirstClick = false;
                isSecondClick = true;
                return;
            }

            if (isSecondClick)
            {
                isSecondClick = false;
                // Aktif olan öğeyi kapatıyoruz
                textElements[currentIndex].gameObject.SetActive(false);

                // Bir sonraki öğeyi aktif hale getiriyoruz (sıradaki öğeyi gösteriyoruz)
                currentIndex++;
                if (currentIndex >= textElements.Count)
                {
                    // Eğer son öğe gösterildiyse, oyun nesnesini devre dışı bırakabilirsiniz veya
                    // başka bir işlem yapabilirsiniz.
                    nextScene.SetActive(true);
                    currentScene.SetActive(false);
                    gameObject.SetActive(false);
                    return;
                }

                // İkinci sol tıkta işlem yapılıyor
                textElements[currentIndex].gameObject.SetActive(true);
                return;
            }

            if (!isFirstClick && !isSecondClick)
            {
                isFirstClick = true;
                return;
            }
        }
    }
}
