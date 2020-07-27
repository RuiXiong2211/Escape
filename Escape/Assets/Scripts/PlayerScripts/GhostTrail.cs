using UnityEngine;
using DG.Tweening;
using System;

public class GhostTrail : MonoBehaviour
{
    public GameObject move;
    public AnimationScript anim;
    public SpriteRenderer sr;
    public Transform ghostsParent;
    public Color trailColor;
    public Color fadeColor;
    public float ghostInterval;
    public float fadeTime;

    private void Start()
    {
        //anim = FindObjectOfType<AnimationScript>();
        //move = FindObjectOfType<Movement>();
        sr = move.GetComponent<SpriteRenderer>();
    }

    public void ShowGhost()
    {
        Sequence s = DOTween.Sequence();

        for (int i = 0; i < ghostsParent.childCount; i++)
        {
            Transform currentGhost = ghostsParent.GetChild(i);
            s.AppendCallback(()=> currentGhost.position = move.transform.position);
            s.AppendCallback(() => currentGhost.localScale = Movement.side > 0 ? new Vector3(1f, 1f, 1f) : new Vector3(-1f, 1f, 1f));
            //s.AppendCallback(() => currentGhost.GetComponent<SpriteRenderer>().flipX = anim.sr.flipX);
            s.AppendCallback(()=>currentGhost.GetComponent<SpriteRenderer>().sprite = sr.sprite);
            s.Append(currentGhost.GetComponent<SpriteRenderer>().material.DOColor(trailColor, 0));
            s.AppendCallback(() => FadeSprite(currentGhost));
            s.AppendInterval(ghostInterval);
        }
    }

    public void FadeSprite(Transform current)
    {
        current.GetComponent<SpriteRenderer>().material.DOKill();
        current.GetComponent<SpriteRenderer>().material.DOColor(fadeColor, fadeTime);
    }

    public void DisableGhosts()
    {
        for (int i = 0; i < ghostsParent.childCount; i++)
        {
            ghostsParent.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void EnableGhosts()
    {
        for (int i = 0; i < ghostsParent.childCount; i++)
        {
            ghostsParent.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
        }
    }

}