using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public Transform groundCheck;
    public Transform wallCheck;
    public float collisionRadiusGround = 0.25f;
    public float collisionRadiusWall = 0.01f;
    public LayerMask groundLayer;
    
    public bool onGround;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    public int wallSide; 

    public Vector2 bottomOffset,rightOffset, leftOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.OverlapCircle((Vector2)groundCheck.position + bottomOffset, collisionRadiusGround, groundLayer);
        onWall = Physics2D.OverlapCircle((Vector2)wallCheck.position + rightOffset, collisionRadiusWall, groundLayer) 
            || Physics2D.OverlapCircle((Vector2)wallCheck.position + leftOffset, collisionRadiusWall, groundLayer);

        onRightWall = onWall && (Movement.side == 1);
        onLeftWall = onWall && (Movement.side == -1);

        wallSide = onRightWall ? -1 : 1;
    }
}
