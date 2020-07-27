using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject movingitem;
    public float moveSpeed;
    public Transform[] points;
    public Transform currentPoint;
    public int  pointSelection;
    public  float direction;


    void Start()
    {
        currentPoint = points[pointSelection];
        direction = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movingitem.transform.position = Vector3.MoveTowards(movingitem.transform.position, currentPoint.transform.position, moveSpeed * Time.deltaTime);
        direction = currentPoint.transform.position.y - movingitem.transform.position.y;
        if (movingitem.transform.position == currentPoint.transform.position)
        {
            pointSelection++;
            if (pointSelection == points.Length)
            {
                pointSelection = 0;
            }
            currentPoint = points[pointSelection];
        }
    }
}
