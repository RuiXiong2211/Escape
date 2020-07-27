using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] EffectsAudio;
    public GameObject[] BGMAudio;
    public float[] EffectOGVol;
    public float[] BGMOGVol;

    //public AudioSource gameaudio;
    void Start()
    {
        EffectsAudio = GameObject.FindGameObjectsWithTag("Effects");
        BGMAudio = GameObject.FindGameObjectsWithTag("BGM");

        EffectOGVol = OG(EffectsAudio);
        BGMOGVol = OG(BGMAudio);


    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void SetEffectsAudio(float volume)
    {
        for (int i = 0; i < EffectsAudio.Length; i++)
        {
            AudioSource gamesound = EffectsAudio[i].GetComponent<AudioSource>();
            //float ogvolume = gamesound.volume;
            gamesound.volume = (volume / 50) * EffectOGVol[i];
        }
    }

    public void SetBGMAudio(float volume)
    {
        for (int i = 0; i < BGMAudio.Length; i++)
        {
            AudioSource gamesound = BGMAudio[i].GetComponent<AudioSource>();
            float ogvolume = gamesound.volume;
            gamesound.volume = (volume / 50) * BGMOGVol[i];
        }
    }

    public float[] OG(GameObject[] parent)
    {
        float[] result = new float[parent.Length];
        for (int i = 0; i < parent.Length; i++)
        {
            result[i] = parent[i].GetComponent<AudioSource>().volume;
        }

        return result;
    }
}
