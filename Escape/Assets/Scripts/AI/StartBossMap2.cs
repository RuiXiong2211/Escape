using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossMap2 : MonoBehaviour
{
    public GameObject firstCheckpoint;
    public Movement player;
    public stupidai4 boss;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Dummy_Player")
        {
            player.transform.position = firstCheckpoint.transform.position;
            boss.transform.position = firstCheckpoint.transform.position;
            boss.gameObject.SetActive(true);
            player.map2Boss = true;
            PlayerPrefs.SetInt("Map2Boss", 1); 
        }
    }
}
