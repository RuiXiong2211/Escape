using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class stupidai4 : MonoBehaviour
{
    [Header("To assign")]
    public Transform following;
    public float delay;
    public Animator visual;
    public BossCollisoin coll;
    public GameObject nextThing;
    public GameObject[] toDisable;
    public GhostTrail ghost; 

    [Space]
    [Header("Stats")]
    public float xValue;
    public float yValue;
    public bool canStart;
    public static int side;
    public Vector3 offset;
    public bool isMoving;

    [Space]
    public float highestX = 0;
    public float smallestX = 0;
    public float highestY = 0;
    public float smallestY = 0;

    private Queue<Vector3> positionCache;
    private Queue<float> timeCache;
    private GameObject BossCheckPoint;
    private Movement player;

    private void OnEnable()
    {
        GetComponent<BossKill>().enabled = false;
        player = FindObjectOfType<Movement>();

        canStart = false;
        //enabled = false;
        StartCoroutine(StartDelay());

        positionCache = new Queue<Vector3>();
        timeCache = new Queue<float>();

        if (player.currentCheckpoint == null)
        {
            Vector3 position;
            position.x = PlayerPrefs.GetFloat("Load" + PlayerPrefs.GetInt("CurrentLoadNum", 0) + "-position.x");
            position.y = PlayerPrefs.GetFloat("Load" + PlayerPrefs.GetInt("CurrentLoadNum", 0) + "-position.y");
            position.z = PlayerPrefs.GetFloat("Load" + PlayerPrefs.GetInt("CurrentLoadNum", 0) + "-position.z");
            positionCache.Enqueue(position);
        }
        else
        {
            positionCache.Enqueue(player.currentCheckpoint.transform.position);
        }
        
        timeCache.Enqueue(Time.time);

        nextThing.SetActive(true);

        Array.ForEach<GameObject>(toDisable, x => x.SetActive(false));
    }

    // Start is called before the first frame update
    void Start()
    {
        /*
        player = FindObjectOfType<Movement>();

        canStart = false;
        enabled = false;
        StartCoroutine(StartDelay());

        positionCache = new Queue<Vector3>();
        timeCache = new Queue<float>();

        positionCache.Enqueue(following.position);
        timeCache.Enqueue(Time.time);
        */
    }

    private void Update()
    {
        // Changes the facing side of Boss
        if (xValue > 0.01)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            side = 1;
        }
        else if (xValue < -0.01)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            side = -1;
        }

        BossCheckPoint = player.currentCheckpoint;

        isMoving = xValue != 0 || yValue != 0;
        visual.SetFloat("HorizontalAxis", Mathf.Abs(xValue));
        visual.SetFloat("VerticalAxis", yValue);
        visual.SetBool("ShouldIdle", xValue == 0);
        visual.SetBool("OnGround", coll.onGround);
        visual.SetBool("OnWall", coll.onWall);
        visual.SetBool("IsMoving", isMoving);

        if (xValue > .5 || yValue > .4 || xValue < -.5 || yValue < -.4)
        {
            ghost.ShowGhost();
        }
    }

    void LateUpdate()
    {
        if (Time.timeScale != 0f)
        {
            // Insert new positions into cache
            positionCache.Enqueue(following.position + offset);
            timeCache.Enqueue(Time.time);

            // Only start dequeing when delay is reached
            float targetTime = Time.time - delay;
            float oldestTime = timeCache.Peek();
            Vector3 oldPos = transform.position;
            Vector3 newPos = transform.position;
            float oldTime = Time.time;
            float newTime = 0f;

            if (oldestTime < targetTime)
            {
                newPos = positionCache.Dequeue();
                newTime = timeCache.Dequeue();
            }

            // Moves boss into new position
            float span = oldTime - newTime;
            float progress = 0f;
            if (span > 0)
            {
                progress = (oldTime - targetTime) / span;
            }

            transform.position = Vector3.Lerp(oldPos, newPos, progress);

            // Keep track of "vector" of boss
            Vector3 temp = newPos - oldPos;
            xValue = temp.x;
            yValue = temp.y;

            SetHighest(xValue, yValue);
        }

        // Animation
        //visual.SetFloat("HorizontalAxis", Mathf.Abs(xValue));
        //visual.SetFloat("VerticalAxis", Mathf.Abs(yValue));
        //visual.SetBool("ShouldIdle", xValue == 0);
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(1f);
        //Debug.Log("Reached");
        canStart = true;
        //enabled = true;
        GetComponent<BossKill>().enabled = true;
    }

    public void SetHighest(float currentX, float currentY)
    {
        float x = Mathf.Abs(currentX);
        float y = Mathf.Abs(currentY);

        if (x == 0 || y == 0)
        {
            return;
        } 
        else
        {
            if (smallestX == 0)
            {
                smallestX = x;
            }
            if (smallestY == 0)
            {
                smallestY = y;
            }
            if (highestX == 0)
            {
                highestX = x;
            }
            if (highestY == 0)
            {
                highestY = y;
            }

            if (x < smallestX)
            {
                smallestX = x;
            }
            if (y < smallestY)
            {
                smallestY = y;
            }
            if (x > highestX)
            {
                highestX = x;
            }
            if (y > highestY)
            {
                highestY = y;
            }
        }
    }
}
