using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameMenu : MonoBehaviour
{
    public void CreateNewGame(int loadNumber)
    {
        PlayerPrefs.SetInt("currentLoad", loadNumber);
        PlayerPrefs.SetInt("newGame", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
