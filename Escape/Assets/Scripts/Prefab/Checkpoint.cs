using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Movement move;

    // Start is called before the first frame update
    void Start()
    {
        move = FindObjectOfType<Movement>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Dummy_Player")
        {
            move.currentCheckpoint = gameObject;    
            move.SavePlayer();
        }
    }
}
