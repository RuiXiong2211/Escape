using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveName : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text toChange;

    public void ChangeLoadName(int loadNum)
    {
        Load.isCreated[loadNum - 1] = true;
        toChange.text = " " + inputField.text;
        PlayerPrefs.SetString("Load" + loadNum + "Text", " " + inputField.text);
        PlayerPrefs.SetInt("CurrentLoadNum", loadNum);
        Load.StartNewGame();
    }
}
