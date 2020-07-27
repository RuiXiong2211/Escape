using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stupidai3 : MonoBehaviour
{
    public Animator anim;
    public Collision coll;

    const int MAX_FPS = 60;

    public Transform leader;
    public float lagSeconds = 0.5f;

    Vector3[] _positionBuffer;
    float[] _timeBuffer;
    int _oldestIndex;
    int _newestIndex;

    public float xvalue;
    public float timeDelay;

    // Use this for initialization
    void Start()
    {
        //body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collision>();

        int bufferLength = Mathf.CeilToInt(lagSeconds * MAX_FPS);
        _positionBuffer = new Vector3[bufferLength];
        _timeBuffer = new float[bufferLength];

        _positionBuffer[0] = _positionBuffer[1] = leader.position;
        _timeBuffer[0] = _timeBuffer[1] = Time.time;

        _oldestIndex = 0;
        _newestIndex = 1;
    }


    void LateUpdate()
    {
        if(xvalue > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (xvalue < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        // Insert newest position into our cache.
        // If the cache is full, overwrite the latest sample.
        int newIndex = (_newestIndex + 1) % _positionBuffer.Length;
        if (newIndex != _oldestIndex)
            _newestIndex = newIndex;

        _positionBuffer[_newestIndex] = leader.position;
        _timeBuffer[_newestIndex] = Time.time;

        // Skip ahead in the buffer to the segment containing our target time.
        float targetTime = Time.time - lagSeconds;
        int nextIndex;
        while (_timeBuffer[nextIndex = (_oldestIndex + 1) % _timeBuffer.Length] < targetTime)
        {
            _oldestIndex = nextIndex;
        }

        // Interpolate between the two samples on either side of our target time.
        float span = _timeBuffer[nextIndex] - _timeBuffer[_oldestIndex];
        float progress = 0f;
        if (span > 0f)
        {
            progress = (targetTime - _timeBuffer[_oldestIndex]) / span;
        }

        xvalue = (_positionBuffer[nextIndex] - _positionBuffer[_oldestIndex]).x;

        transform.position = Vector3.Lerp(_positionBuffer[_oldestIndex], _positionBuffer[nextIndex], progress);

        /*
        //Animation
        anim.SetFloat("Speed", Mathf.Abs(xvalue));
        anim.SetFloat("Jump.y", body.velocity.y);
        anim.SetBool("onGround", coll.onGround);
        anim.SetBool("Climb", coll.onWall && body.velocity.y == 0);
        anim.SetBool("wallSlide", coll.onWall && body.velocity.y < 0);
        */
    }
}