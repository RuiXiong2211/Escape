using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindResistance : MonoBehaviour
{
    public Movement move;
    public float direction;
    public bool check = false;
    private Collision coll;

    // Start is called before the first frame update
    void Start()
    {
        move = FindObjectOfType<Movement>();
        coll = FindObjectOfType<Collision>();
    }

    private void Update()
    {
        if (check)
        {
            move.WindWalk(direction);
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Dummy_Player")
        {
            if (coll.onWall)
            {
                move.airResistance = false;
                move.speedUp = false;
                check = false;
            }
            else
            {
                if (move.direction < 0 && direction < 0)
                {
                    move.airResistance = false;
                    move.speedUp = true;
                    check = false;
                }
                else if (move.direction > 0 && direction < 0)
                {
                    move.airResistance = true;
                    move.speedUp = false;
                    check = false;
                }
                else if (move.direction < 0 && direction > 0)
                {
                    move.airResistance = true;
                    move.speedUp = false;
                    check = false;
                }
                else if (move.direction > 0 && direction > 0)
                {
                    move.airResistance = false;
                    move.speedUp = true;
                    check = false;
                }
                else if (move.direction == 0)
                {
                    check = true;
                }
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Dummy_Player")
        {
            move.airResistance = false;
            move.speedUp = false;
            check = false;
        }
    }
}
