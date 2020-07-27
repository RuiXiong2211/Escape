using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touchcoin : MonoBehaviour
{
    // Start is called before the first frame update
    public bool hasTouched;
    public bool firstTime;
    public Animator anim;
    public Movement player;
    public Touchcoinbox cockbox;
    public RoomTransition camerai;

    void Start()
    {
        anim = GetComponent<Animator>();
        hasTouched = false;
        firstTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("hasTouched", hasTouched);
        Reset();
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.name == "Dummy_Player") {
            Debug.Log("Hi");
            hasTouched = true;
            //firstTime = false;
        }
    }

    /* method that checks if the next room has been unlocked,
     * if it has not been unlocked, touch coins will  be reset upon 
     * death of the player.*/
    public void Reset()
    {
        //if (!player.isAlive && !cockbox.canOpen) {
        //    hasTouched = false;
        //}
        if (!camerai.roomUnlocked && !player.isAlive)
        {
            hasTouched = false;
            firstTime = true;
        }
    }
}
