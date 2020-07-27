using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DashReset : MonoBehaviour
{
    public Movement player;
    public float respawnDelay;
    public SpriteRenderer sr;
    public AudioSource crystal;
    void Start()
    {
        player = FindObjectOfType<Movement>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Dummy_Player")
        {
            crystal.Play();
            player.hasDashed = false;
            gameObject.SetActive(false);
            Invoke("Respawn", 3);
        }
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
    }

    public void CrystalSound()
    {
        crystal.Play();
    }

}
