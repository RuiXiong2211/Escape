using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneTransition : MonoBehaviour
{
    //public string sceneName;
    public Animator TransitionAnim;
    public bool bossFightDone;
    public GameObject[] toEnable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bossFightDone)
        {
            Array.ForEach<GameObject>(toEnable, x => x.SetActive(true));
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Dummy_Player")
        {
            PlayerPrefs.SetInt("Map2Boss", 0);
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator LoadScene()
    {
        TransitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(2f);
        PlayerPrefs.SetInt("NewGame", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
