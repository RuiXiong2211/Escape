using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Buttons : MonoBehaviour
{
    public Image imgComp;
    public Button butComp;

    // Start is called before the first frame update
    void Start()
    {
        imgComp = GetComponent<Image>();
        butComp = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateSprite()
    {
        imgComp.color = new Color32(255, 255, 255, 255);
    }

    public void DeactivateSprite()
    {
        imgComp.color = new Color32(255, 255, 255, 0);
    }

}
