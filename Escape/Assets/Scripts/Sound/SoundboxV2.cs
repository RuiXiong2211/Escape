using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundboxV2 : MonoBehaviour

{
    // Start is called before the first frame update
    public bool playerInBox;
    public bool notPlayedYet;
    public AudioSource bgm;
    public AudioSource secondbgm;
    public float startVolume;
    public bool first;
    public bool map2boss;
    void Start()
    {
        playerInBox = false;
        notPlayedYet = true;
        startVolume = (PlayerPrefs.GetFloat("CurrBGMVol", 50) / 50) * bgm.volume;
        first = true;
    }

    // Update is called once per frame
    void Update()
    {
        map2boss = PlayerPrefs.GetInt("Map2Boss", 0) == 1;
        if (playerInBox && notPlayedYet)
        {
            bgm.volume = startVolume;
            secondbgm.volume = startVolume;
            if (!map2boss)
            {
                bgm.Play();
            }
            else if (map2boss)
            {
                bgm.Stop();
                secondbgm.Play();
            }
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
