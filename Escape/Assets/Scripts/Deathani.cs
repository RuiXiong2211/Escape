using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathani : MonoBehaviour
{
    // Start is called before the first frame update
    public Movement player;
    public Animator anim;
    public AudioSource audiosound;
    void Start()
    {
        player = FindObjectOfType<Movement>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isAlive", player.isAlive);
    }

    void playDeath()
    {
        audiosound.Play();
    }
}
