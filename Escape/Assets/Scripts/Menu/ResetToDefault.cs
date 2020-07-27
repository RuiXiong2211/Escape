using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResetToDefault : MonoBehaviour
{
    public TMP_Text Text1;
    public TMP_Text Text2;
    public TMP_Text Text3;

    public void Restart()
    {
        PlayerPrefs.DeleteAll();
        Load.isCreated = new bool[] { false, false, false };
        Text1.text = " ";
        Text2.text = " ";
        Text3.text = " ";
    }
}
