using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KillPlayer : MonoBehaviour
{
    public GameObject spikeCheckpoint;
    public Movement player;
    public GhostTrail ghost;
    public DashReset[] resets;

    //public float respawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Movement>();
        ghost = FindObjectOfType<GhostTrail>();
        resets = FindObjectsOfType<DashReset>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator RespawnPlayer()
    {
        player.inBubble = false;
        player.isAlive = false;
        //player.inBubble = false;
        player.soundplayed = false;
        StartCoroutine(player.DisableNStopMovement(1.2f));
        player.enabled = false;
        ghost.DisableGhosts();
        player.GetComponent<Renderer>().enabled = false;
        //player.isAlive = false;
        //player.inBubble = false;
        yield return new WaitForSeconds(1.2f);
        player.isAlive = true;
        player.transform.position = spikeCheckpoint.transform.position;
        player.enabled = true;
        ghost.EnableGhosts();
        player.GetComponent<Renderer>().enabled = true;
        Array.ForEach<DashReset>(resets, r => r.Respawn());
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (player.map2Boss)
        {
            StartCoroutine(FindObjectOfType<BossKill>().RespawnPlayer());
        }
        else if (other.name == "Dummy_Player")
        {
            StartCoroutine(RespawnPlayer());
            //StartCoroutine(player.DisableNStopMovement(respawnDelay));
            player.deathParticle.Play();
        }

        
    }
}
