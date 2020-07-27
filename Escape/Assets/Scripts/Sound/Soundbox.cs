using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundbox : MonoBehaviour

{
    // Start is called before the first frame update
    public bool playerInBox;
    public bool notPlayedYet;
    public AudioSource bgm;
    public float startVolume;
    void Start()
    {
        playerInBox = false;
        notPlayedYet = true;
        startVolume = (PlayerPrefs.GetFloat("CurrBGMVol", 50) / 50) * bgm.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInBox && notPlayedYet)
        {
            bgm.volume = startVolume;
            bgm.Play();
            notPlayedYet = false;
        }

        if (!playerInBox)
        {   
            // fade out music
            bgm.volume -= startVolume * Time.deltaTime / 4f;
            //bgm.Stop();
            notPlayedYet = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerInBox = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerInBox = false;
        }
    }
}
