using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int Scenenumber;

    private bool startPressedFlag = false;

    public bool StartPressedFlag
    {
        get => startPressedFlag;
        private set
        {
            startPressedFlag = value;
        }
    }

    // Wait with playing until ResumeGame method has been called
    private void Start()
    {
        Scenenumber = SceneManager.GetActiveScene().buildIndex;

        AudioManager.instance.PauseAllFmodEvents();
        Time.timeScale = 0;
    }

    // Pauses the game
    public void PauseGame()
    {
        AudioManager.instance.PauseAllFmodEvents();
        Time.timeScale = 0;
        StartPressedFlag = false;
    }

    //Starts or resumes the game
    public void ResumeGame ()
    {
        Time.timeScale = 1;
        AudioManager.instance.ResumeAllFmodEvents();

        StartPressedFlag = true;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(Scenenumber);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}