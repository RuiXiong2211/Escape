using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flightbubble : MonoBehaviour
{
    public Movement player;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Dummy_Player")
        {
            player.inBubble = true;
            player.soundplayed = false;
            player.BubbleTime = player.defaultBubbleTime;
            StartCoroutine(poofWait());
            anim.SetBool("Poof", player.inBubble);
            // insert soundtrack
            Invoke("Respawn", 2);
        }
    }

        public void Respawn()
    {
        gameObject.SetActive(true);
    }

    IEnumerator poofWait()
    {
        yield return new WaitForSeconds(0.65f);
        gameObject.SetActive(false);
    }
}
