using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public GameObject savedCheckpoint;

    public PlayerData(Movement player)
    {
        savedCheckpoint = player.currentCheckpoint;
    }

    public PlayerData()
    {
        savedCheckpoint = GameObject.Find("Checkpoint 1");
    }
}
