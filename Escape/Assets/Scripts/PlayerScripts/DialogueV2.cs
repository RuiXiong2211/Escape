using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Difference from DialogueV2 include an automatic NPC dialogue that only occurs once that player enters triggerzone. No need press Buttons.
public class DialogueV2 : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public BigBrainClass2[] sentences;
    public int index;
    public float typingSpeed;
    public Movement player;
    public bool hasTalked;
    public GameObject textbox;
    public GameObject continueButton;
    public GameObject ghost;
    public GameObject BossIMG;

    // Start is called before the first frame update
    void Start()
    {
        hasTalked = false;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Dummy_Player")
        {
            if (!hasTalked)
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
        if (textDisplay.text == sentences[index].text || textDisplay.text == sentences[0].text)
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

        if (sentences[index].italic)
        {
            textDisplay.fontStyle = FontStyles.Italic;
        }
        else
        {
            textDisplay.fontStyle = FontStyles.Normal;
        }

        if (sentences[index].boss)
        {
            BossIMG.SetActive(true);
        }
        else
        {
            BossIMG.SetActive(false);
        }

        textDisplay.text += sentences[index].text;
        yield return new WaitForSeconds(typingSpeed);
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            continueButton.SetActive(false);
            textbox.SetActive(false);
            BossIMG.SetActive(false);
            //enables player movements;
            player.canMove = true;
            //resets dialogue
            index = 0;
            BossIMG.SetActive(false);
            StartCoroutine(fadeOut(ghost, 3));
        }
    }


    IEnumerator fadeOut(GameObject MyRenderer, float duration)
    {
        float counter = 0;
        //Get current color
        Color spriteColor = MyRenderer.GetComponent<SpriteRenderer>().material.color;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            //Fade from 1 to 0
            float alpha = Mathf.Lerp(1, 0, counter / duration);
            Debug.Log(alpha);

            //Change alpha only
            MyRenderer.GetComponent<SpriteRenderer>().material.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            //Wait for a frame
            yield return null;
        }
    }
}

[System.Serializable]
public class BigBrainClass2
{
    public string text;
    public bool italic;
    public bool boss;

    BigBrainClass2(string text, bool italic, bool boss)
    {
        this.text = text;
        this.italic = italic;
        this.boss = boss;
    }
}
