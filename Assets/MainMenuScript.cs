/*
This Script Loads first.
It has logic to load levels based on button click ,
It also changes language of 
    - intructions 
 
 */
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    
    public Button playButton; // The Play button for level 1
    public Button playButton2; // The Play button for level 2
    public Button playButton3; // The Play button for level 3
    public Button playButton4; // The Play button for level 4
    public Button exitButton; // The Exit button

    void Start()
    {
        // Attach UI button click listeners
        playButton.onClick.AddListener(PlayButtonClick);
        playButton2.onClick.AddListener(PlayButton2Click);
        playButton3.onClick.AddListener(PlayButton3Click);
        playButton4.onClick.AddListener(PlayButton4Click);
        exitButton.onClick.AddListener(ExitButtonClick);

    }

    void PlayButtonClick()
    {
        SceneManager.LoadScene("level1");
    }
    void PlayButton2Click()
    {
        SceneManager.LoadScene("level2");
    }
    void PlayButton3Click()
    {
        SceneManager.LoadScene("level3");
    }
    void PlayButton4Click()
    {
        SceneManager.LoadScene("level4");
    }

    void ExitButtonClick()
    {
        // Quit the application
        Application.Quit();
    }

}