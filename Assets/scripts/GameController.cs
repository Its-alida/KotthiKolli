using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject targetObject; // The game object to disappear
    public AudioSource audioToStop; // The audio source to stop
    public AudioSource audioToPlay; // The audio source to play
    public Button playButton; // The Play button
    public Button exitButton; // The Exit button
    public AudioSource kolliAudio; // The AudioSource for "kolli.mp3"
    public AudioSource kothiAudio; // The AudioSource for "kothi.mp3"
    public AudioSource rightAudio; // The AudioSource for "right.mp3"
    public AudioSource wrongAudio; // The AudioSource for "wrong.mp3"
    public GameObject monkey; // The monkey game object
    public GameObject hen; // The hen game object
    public TMP_Text scoreText; // TMP_Text for displaying score

    private bool playKolli = true;
    private int score = 0;
    private bool isKothiPlaying = false;
    private bool isKolliPlaying = false;

    void Start()
    {
        // Assign the PlayButtonClick function to the Play button's onClick event
        playButton.onClick.AddListener(PlayButtonClick);
        // Assign the ExitButtonClick function to the Exit button's onClick event
        exitButton.onClick.AddListener(ExitButtonClick);

        // Initialize score
        UpdateScore();
    }

    void PlayButtonClick()
    {
        // Make the target object disappear
        targetObject.SetActive(false);

        // Stop the audio that should stop
        audioToStop.Stop();

        // Start the audio that should play
        audioToPlay.Play();

        // Start the coroutine to play audio commands and swap objects
        StartCoroutine(PlayAudioCommands());
    }

    void ExitButtonClick()
    {
        // Quit the application
        Application.Quit();
    }

    IEnumerator PlayAudioCommands()
    {
        // Wait for the initial audio to finish
        yield return new WaitForSeconds(audioToPlay.clip.length);

        while (true)
        {
            // Play the alternating audio clips
            if (playKolli)
            {
                kolliAudio.Play();
                isKolliPlaying = true;
                yield return new WaitForSeconds(kolliAudio.clip.length);
                isKolliPlaying = false;
            }
            else
            {
                kothiAudio.Play();
                isKothiPlaying = true;
                yield return new WaitForSeconds(kothiAudio.clip.length);
                isKothiPlaying = false;
            }

            // Swap the positions of monkey and hen
            SwapPositions(monkey, hen);

            // Wait for 7 seconds before playing the next audio command
            yield return new WaitForSeconds(7f);

            // Alternate the audio clip to be played next
            playKolli = !playKolli;
        }
    }

    void SwapPositions(GameObject obj1, GameObject obj2)
    {
        Vector3 tempPosition = obj1.transform.position;
        obj1.transform.position = obj2.transform.position;
        obj2.transform.position = tempPosition;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isKothiPlaying)
            {
                if (other.gameObject.CompareTag("Monkey"))
                {
                    Debug.Log("Monkey Now");
                    if (!rightAudio.isPlaying) // Ensure not to overlap audio
                        rightAudio.Play();
                    score++;
                    UpdateScore();
                }
                else if (other.gameObject.CompareTag("Hen"))
                {
                    if (!wrongAudio.isPlaying) // Ensure not to overlap audio
                        wrongAudio.Play();
                }
            }
            else if (isKolliPlaying)
            {
                if (other.gameObject.CompareTag("Hen"))
                {
                    if (!rightAudio.isPlaying) // Ensure not to overlap audio
                        rightAudio.Play();
                    score++;
                    UpdateScore();
                }
                else if (other.gameObject.CompareTag("Monkey"))
                {
                    if (!wrongAudio.isPlaying) // Ensure not to overlap audio
                        wrongAudio.Play();
                }
            }
        }
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
