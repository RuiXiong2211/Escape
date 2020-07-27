using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public RoomTransition room;
    public AudioSource campfire;
    public bool notplayedyet;
    // Start is called before the first frame update
    void Start()
    {
        notplayedyet = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (room.playerInRoom && notplayedyet)
        {
            campfire.Play();
            notplayedyet = false;
        }

        if (!room.playerInRoom)
        {
            campfire.Stop();
            notplayedyet = true;
        }
    }
}
