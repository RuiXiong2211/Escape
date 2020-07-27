using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    public static bool[] isCreated;
    public GameObject PlayMenu;
    public GameObject CreateNames;
    public TMP_Text thisText;
    public int thisLoadNum;

    
    private void Start()
    {
        isCreated = new bool[3];
        if (PlayerPrefs.GetInt("FirstLoad", 1) == 1)
        {
            PlayerPrefs.SetInt("FirstLoad", 0);
            PlayerPrefs.SetInt("Load1Created", 0);
            PlayerPrefs.SetInt("Load2Created", 0);
            PlayerPrefs.SetInt("Load3Created", 0);
        }

        isCreated[0] = PlayerPrefs.GetInt("Load1Created", 0) == 1 ? true : false;
        isCreated[1] = PlayerPrefs.GetInt("Load2Created", 0) == 1 ? true : false;
        isCreated[2] = PlayerPrefs.GetInt("Load3Created", 0) == 1 ? true : false;

        thisText.text = GetText(thisLoadNum);
    }

    public void CreateLoad(int loadNum)
    {
        int index = loadNum - 1;

        if (isCreated[index])
        {
            Debug.Log("Created already");
            PlayerPrefs.SetInt("CurrentLoadNum", loadNum);
            int sceneNum = PlayerPrefs.GetInt("Load" + loadNum + "SceneNum", 2);
            Debug.Log("SceneNum:" + PlayerPrefs.GetInt("Load" + loadNum + "SceneNum") + "/" + loadNum);
            LoadGame(sceneNum);
        } 
        else
        {
            PlayMenu.SetActive(false);
            CreateNames.SetActive(true);
            PlayerPrefs.SetInt("Load" + loadNum + "Created", 1);
        }
    }
    
    public string GetText(int loadNum)
    {
        return PlayerPrefs.GetString("Load" + loadNum + "Text", " ");
    }

    public static void StartNewGame()
    {
        PlayerPrefs.SetInt("Load" + PlayerPrefs.GetInt("CurrentLoadNum") + "Created", 1);
        PlayerPrefs.SetInt("NewGame", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.SetInt("Load" + PlayerPrefs.GetInt("CurrentLoadNum") + "SceneNum", 1);
    }

    public void LoadGame(int sceneNum)
    {
        PlayerPrefs.SetInt("NewGame", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + sceneNum);
    }

}
