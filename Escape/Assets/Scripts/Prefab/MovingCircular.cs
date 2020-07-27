using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCircular : MonoBehaviour
{
    public float rotateSpeed;
    public float radius;
    public Vector2 center;
    public float angle;
    public float time;
    public bool clockWise;
    private Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        center = transform.position;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        angle = rotateSpeed * time;
        
        if (clockWise)
        {
            offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * radius;
        } 
        else
        {
            offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        }
        
        transform.position = center + offset;
    }
}
