using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public Movement move;
    public float windForce;
    void Start()
    {
        move = FindObjectOfType<Movement>();
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Dummy_Player")
        {
            move.player.velocity = new Vector2(move.player.velocity.x, 0);
            move.player.AddForce(Vector2.up* windForce, ForceMode2D.Impulse);
        }
    }
}
