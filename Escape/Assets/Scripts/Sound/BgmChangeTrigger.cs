using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmChangeTrigger : MonoBehaviour
{
    public SoundboxV2 soundboxv2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Dummy_Player")
        {
            soundboxv2.first = false;
        }
    }
}
