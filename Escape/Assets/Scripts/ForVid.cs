using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForVid : MonoBehaviour
{
    public Rigidbody2D player;
    public float moveSpeed = 7;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y);

        Walk(dir);
    }

    private void Walk(Vector2 dir)
    {
        player.velocity = new Vector2(dir.x * moveSpeed, dir.y * moveSpeed);
    }
}
