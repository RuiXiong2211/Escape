using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;   
    public GameObject pauseMenuUI;
    public Movement move;
    public GameObject optionsMenuUI;

    private void Start()
    {
        move = FindObjectOfType<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                ResumeGame();
            } else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        move.SavePlayer();
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void PauseGame()
    {
        move.SavePlayer();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(0);
        move.SavePlayer();
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        move.transform.position = move.currentCheckpoint.transform.position;
        ResumeGame();
    }
}
