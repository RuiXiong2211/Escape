using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class SceneTransition2 : MonoBehaviour
{
    public bool totransit;
    public String sceneName;
    public float time;
    void Start()
    {
        totransit = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (totransit)
        {
            totransit = false;
            StartCoroutine(LoadScene());
        }
    }



    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(time);
        PlayerPrefs.SetInt("NewGame", 1);
        SceneManager.LoadScene(sceneName);
            //SceneManager.GetActiveScene().buildIndex + 1);
    }
}
