using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SpringJump : MonoBehaviour
{
    public Movement move;
    public float springForce;
    private Animator anim;
    public AudioSource springsound;

    // Start is called before the first frame update
    void Start()
    {
        move = FindObjectOfType<Movement>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Dummy_Player")
        {
            anim.SetTrigger("hitSpring");
            move.player.velocity = new Vector2(move.player.velocity.x, 0);
            move.player.AddForce(Vector2.up * springForce, ForceMode2D.Impulse);
            //move.player.velocity += Vector2.up * springForce;
        }
    }

    void Springsound()
    {
        springsound.Play();
    }
}
