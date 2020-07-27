using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;

public class stupidai : MonoBehaviour
{
    [Header("Modifiables")]
    public GameObject respawnPos;
    public float nextTime = 2.5f;
    public RoomTransition chasingRoom;
    public GhostTrail ghost;
    public Vector3 ogpos;
    public AudioSource roar;
    public bool firstEncount;
    public bool isCharging;
    public bool missTarget;

    [Space]
    [Header("These should be private when tidying")]
    private Movement player;
    public Rigidbody2D rb;
    private Animator anim;
    private Vector2 force;
    private float speed = 1650f;
    private Vector2 currentPos;
    private Vector2 playerPos;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Movement>();
        ogpos = transform.position;
        firstEncount = true;
        nextTime = 2.5f;
    }

    // Update is called once per frame

    private void Update()
    {   
        if (chasingRoom.playerInRoom)
        {
            anim.SetBool("isCharging", isCharging);
            // fix for first encounter where boss would immediately attack player upon entering room.
            if (firstEncount && chasingRoom.playerInRoom)
            {
                nextTime -= Time.deltaTime;
                if (nextTime < 0)
                {
                    TrackPlayer();
                    firstEncount = false;
                }
            }
            // extrapolates the player x and y position in order to track the player, before attacking, 
            // moves towards y position faster than x position of player.
            if (!isCharging && chasingRoom.playerInRoom)
            {
                transform.position = Vector3.Lerp(transform.position,
                    new Vector3(transform.position.x, player.transform.position.y, transform.position.z), 2f * Time.deltaTime);
                transform.position = Vector3.Lerp(transform.position,
                    new Vector3(player.transform.position.x, transform.position.y, transform.position.z), .5f * Time.deltaTime);
            }
            if (chasingRoom.ghostmiss)
            {
                chasingRoom.ghostmiss = false;
                Invoke("resetVelocity", 2f);
            }
        }
        if (!chasingRoom.playerInRoom && chasingRoom.roomUnlocked)
        {
            //transform.position = ogpos;
            this.gameObject.SetActive(false);
        }
    }
    
    void TrackPlayer()
    {
        roar.Play();
        force = Vector2.right * speed * Time.deltaTime;
        //isCharging = true;
        StartCoroutine(ChargeDelay());
    }

    void resetVelocity()
    { 
        rb.velocity = Vector2.zero;
        gameObject.transform.position = new Vector3(respawnPos.transform.position.x, player.transform.position.y, player.transform.position.z);
        isCharging = false;
        firstEncount = true;
        nextTime = 2.5f;
    }

    IEnumerator ChargeDelay()
    {
        isCharging = true;
        yield return new WaitForSeconds(0.2f);
        //add audio
        rb.velocity += force;
        ghost.ShowGhost();
        //Invoke("resetVelocity", 4f);
    }

    //IEnumerator firstDelay()
    //{
    //    yield return new WaitForSeconds(5f);
    //    TrackPlayer();
    //    firstEncount = false;
    //}

}
