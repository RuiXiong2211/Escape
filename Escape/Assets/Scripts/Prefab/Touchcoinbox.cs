using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touchcoinbox : MonoBehaviour
{
    // Start is called before the first frame update
    public bool canOpen;
    public int numberofcoins;
    public Touchcoin[] allCoins;
    public Transform nextPos;
    public float moveSpd;
    public bool firstTime;
    public Movement player;
    public Animator anim;
    public RoomTransition roomis;
    public Vector3 originalposition;
    public AudioSource open;

    public float t = 1f;
    void Start()
    {
        canOpen = false;
        firstTime = true;
        anim = GetComponent<Animator>();
        originalposition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //method that checks if all the coins have been touched.
        anim.SetBool("canMove", canOpen);
        Touched();
        if (canOpen && firstTime)
        {
            MoveBox();
        }

        // Resets box position upon player's death.
        Reset();
    }

    IEnumerator OpenWait() 
    {
        player.currentCam.ShakeCamera(2f, 0.5f);
        open.Play();
        yield return new WaitForSeconds(0.8f);
        transform.position = Vector3.Lerp(transform.position, nextPos.position, moveSpd * Time.deltaTime);
        firstTime = false;
    }

    public void MoveBox()
    {
        StartCoroutine(OpenWait());
    }

    public bool Touched()
    {  
       for (int i = 0; i < numberofcoins; i++)
        {
            if (allCoins[i].hasTouched == false)
            {
                canOpen = false;
                break;
            } else
            {
                canOpen = true;
            }
        }
        return canOpen;
   }

    public void Reset()
    {
        if (!player.isAlive && !roomis.roomUnlocked) {
            transform.position = originalposition;
            firstTime = true;
        }
    }
}
