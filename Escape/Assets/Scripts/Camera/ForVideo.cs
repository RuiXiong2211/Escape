using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForVideo : MonoBehaviour
{
    public GameObject virtualcam;
    public bool roomUnlocked;
    public bool playerInRoom;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GG") /*&& !other.isTrigger*/)
        {
            virtualcam.SetActive(true);
            roomUnlocked = true;
            playerInRoom = true;
        }
    }

    private void Start()
    {
        roomUnlocked = false;
        playerInRoom = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("GG") /*&& !other.isTrigger*/)
        {
            virtualcam.SetActive(false);
            playerInRoom = false;
        }
    }

}
