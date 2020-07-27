using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;

public class CameraShake : MonoBehaviour
{   
    public static CameraShake Instance { get; private set; }
    public CinemachineVirtualCamera cm;
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        cm = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cmp = cm.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cmp.m_AmplitudeGain = intensity;
        startingIntensity = intensity;
        shakeTimerTotal = time;
        shakeTimer = time;
    }
    // Update is called once per frame
    void Update()
    {   if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
        }
        if (shakeTimer <= 0f)
        {
            CinemachineBasicMultiChannelPerlin cmp = cm.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            //cmp.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f,  1 - shakeTimer / shakeTimerTotal);

            cmp.m_AmplitudeGain = 0f;
        }
    }
}
