using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKill : MonoBehaviour
{
    public GameObject spikeCheckpoint;
    public Movement player;
    public GhostTrail ghost;
    public GameObject boss;

    private void OnEnable()
    {
        //player = FindObjectOfType<Movement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //player = FindObjectOfType<Movement>();
        //ghost = FindObjectOfType<GhostTrail>();
    }

    public IEnumerator RespawnPlayer()
    {
        spikeCheckpoint = player.currentCheckpoint;
        player.isAlive = false;
        player.inBubble = false;
        StartCoroutine(player.DisableNStopMovement(1.2f));
        player.enabled = false;
        ghost.DisableGhosts();
        player.GetComponent<Renderer>().enabled = false;
        boss.GetComponent<stupidai4>().enabled = false;
        boss.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(1.2f);
        player.isAlive = true;
        player.transform.position = spikeCheckpoint.transform.position;
        boss.transform.position = spikeCheckpoint.transform.position;
        player.enabled = true;
        ghost.EnableGhosts();
        player.GetComponent<Renderer>().enabled = true;
        StartCoroutine(RespawnBoss());
        boss.GetComponent<stupidai4>().enabled = true;
        //boss.GetComponent<Renderer>().enabled = true;
    }

    IEnumerator RespawnBoss()
    {
        yield return new WaitForSeconds(0.3f);
        //boss.GetComponent<stupidai4>().enabled = true;
        boss.GetComponent<Renderer>().enabled = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Dummy_Player" && boss.GetComponent<stupidai4>().canStart)
        {
            //spikeCheckpoint = player.currentCheckpoint;
            StartCoroutine(RespawnPlayer());
            //StartCoroutine(player.DisableNStopMovement(respawnDelay));
            player.deathParticle.Play();
        }
    }
}
