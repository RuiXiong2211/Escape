using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKill2 : MonoBehaviour
{
    public GameObject spikeCheckpoint;
    public Movement player;
    public GhostTrail ghost;
    public stupidai boss;

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
        player.isAlive = false;
        player.inBubble = false;
        boss.isCharging = false;
        ghost.DisableGhosts();
        boss.firstEncount = true;
        boss.nextTime = 100f;
        player.GetComponent<Renderer>().enabled = false;
        boss.GetComponent<stupidai>().enabled = false;
        boss.GetComponent<Renderer>().enabled = false;
        boss.rb.velocity = Vector2.zero;
        StartCoroutine(player.DisableNStopMovement(1.2f));
        player.enabled = false;
        ghost.DisableGhosts();
        //player.GetComponent<Renderer>().enabled = false;
        //boss.GetComponent<stupidai>().enabled = false;
        //boss.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(1.2f);
        boss.nextTime = 2.5f;
        player.isAlive = true;
        player.transform.position = spikeCheckpoint.transform.position;
        boss.transform.position = boss.ogpos;
        player.enabled = true;
        ghost.EnableGhosts();
        player.GetComponent<Renderer>().enabled = true;
        boss.GetComponent<stupidai>().enabled = true;
        boss.GetComponent<Renderer>().enabled = true;
        //boss.GetComponent<Renderer>().enabled = true;
    }

    IEnumerator RespawnBoss()
    {
        yield return new WaitForSeconds(0f);
        //boss.GetComponent<stupidai4>().enabled = true;
        boss.GetComponent<Renderer>().enabled = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Dummy_Player")
        {
            //spikeCheckpoint = player.currentCheckpoint;
            StartCoroutine(RespawnPlayer());
            //StartCoroutine(player.DisableNStopMovement(respawnDelay));
            player.deathParticle.Play();
        }
    }
}
