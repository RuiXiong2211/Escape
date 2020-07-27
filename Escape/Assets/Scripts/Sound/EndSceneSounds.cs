using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneSounds : MonoBehaviour
{
    public AudioSource horn;
    public AudioSource collision;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void hornSound()
    {
        horn.Play();
    }

    void collisionSound()
    {
        collision.Play();
    }
}
