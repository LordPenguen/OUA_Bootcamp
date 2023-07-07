using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public GameObject pauseMenu;
    public bool isPaused;

    private float startVolume = 0.5f;

    private void OnEnable() 
    {
        if(!PlayerPrefs.HasKey("gameVolume")){

        PlayerPrefs.SetFloat("gameVolume",startVolume);

        Load();
        }

        else Load();
    }

    //control Volume of game
    public void SetVolume(){

        AudioListener.volume = volumeSlider.value;
    }

    //Save the players changes
    private void Load(){

        volumeSlider.value = PlayerPrefs.GetFloat("gameVolume");
    }

    private void Save(){

        PlayerPrefs.SetFloat("gameVolume",volumeSlider.value);
    }

    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if(isPaused)
            { 
                ClosePauseMenu();

            }
            else 
            {
                OpenPauseMenu();
            }
        }
    }

    public void OpenPauseMenu(){
        
        Time.timeScale = 0f;
        pauseMenu.gameObject.SetActive(true);
        isPaused=true;
        Debug.Log("menu is open");
    }

    public void ClosePauseMenu(){

        Time.timeScale = 1f;
        pauseMenu.gameObject.SetActive(false);
        isPaused=false;
        Debug.Log("menu is closed");
    }

    public void MainMenu()
    {
        //Main menü sahnesinin numarası yazılcak
        // SceneManager.LoadScene(0);
        Debug.Log("Sent to Main menu");
    }
}
