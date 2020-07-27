using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{

    private Animator anim;
    private Movement move;
    private Collision coll;

    public SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collision>();
        move = GetComponent<Movement>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
