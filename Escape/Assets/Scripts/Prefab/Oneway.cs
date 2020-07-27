using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oneway : MonoBehaviour
{
    public PlatformEffector2D platform;
    // Start is called before the first frame update
    void Start()
    {
        platform = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") <= 0 && Input.GetButtonDown("Fire1"))
        {
            platform.rotationalOffset = 180f;
        }

        //if (Input.GetButtonDown("Jump"))// || Input.GetButtonDown("Fire1"))
        else
        {
            platform.rotationalOffset = 0f;
        }
    }
}
