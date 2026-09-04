using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;

    public void Pause()
    {
        // Show the pause menu and freeze the game.
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        // Hide the pause menu and resume the game.
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
}