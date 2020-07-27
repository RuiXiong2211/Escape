using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    //public string[] sentences;
    public BigBrainClass[] test;
    public int index;
    public float typingSpeed;
    public Movement player;
    public bool hasTalked;
    public GameObject textbox;
    public GameObject continueButton;
    public GameObject playerIMG;
    public GameObject npcIMG;

    // Start is called before the first frame update
    void Start()
    {
        hasTalked = false;
        //test = new BigBrainClass[3];
        //continueButton.SetActive(false);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Dummy_Player")
        {
            if (Input.GetKey(KeyCode.Q) && !hasTalked)
            {
                //disables player movements
                textbox.SetActive(true);
                hasTalked = true;
                player.canMove = false;
                player.player.velocity = Vector2.zero;
                StartCoroutine(Type());
                StartCoroutine(fuckingContinueBox());
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (textDisplay.text == test[index].text || textDisplay.text == test[0].text)
        {
            continueButton.SetActive(true);
        }
    }
    IEnumerator fuckingContinueBox()
    {
        yield return new WaitForSeconds(0.2f);
        continueButton.SetActive(true);
    }

    IEnumerator Type()
    {
        //foreach (char letter in sentences[index].ToCharArray())
        //{
        //    textDisplay.text += letter;
        //    yield return new WaitForSeconds(typingSpeed);
        //}
        if (test[index].italic)
        {
            textDisplay.fontStyle = FontStyles.Italic;
        } 
        else
        {
            textDisplay.fontStyle = FontStyles.Normal;
        }
        
        if (test[index].player)
        {
            playerIMG.SetActive(true);
        }
        else
        {
            playerIMG.SetActive(false);
        }

        if (test[index].npc)
        {
            npcIMG.SetActive(true);
        }
        else
        {
            npcIMG.SetActive(false);
        }

        textDisplay.text += test[index].text;
        yield return new WaitForSeconds(typingSpeed);
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);
        if (index < test.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        } else
        {
            textDisplay.text = "";
            continueButton.SetActive(false);
            textbox.SetActive(false);
            npcIMG.SetActive(false);
            playerIMG.SetActive(false);
            //enables player movements;
            player.canMove = true;
            hasTalked = false;
            //resets dialogue
            index = 0;
        }
    }
}

[System.Serializable]
public class BigBrainClass
{
    public string text;
    public bool italic;
    public bool player;
    public bool npc;

    BigBrainClass(string text, bool italic, bool player, bool npc)
    {
        this.text = text;
        this.italic = italic;
        this.player = player;
        this.npc = npc;
    }
}