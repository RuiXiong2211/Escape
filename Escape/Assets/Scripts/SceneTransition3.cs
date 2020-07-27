using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition3 : MonoBehaviour
{
    public bool totransit;
    public Animator TransitionAnim;

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
        TransitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
