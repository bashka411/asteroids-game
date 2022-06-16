using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }

    private CinemachineVirtualCamera cmVM;
    private CinemachineBasicMultiChannelPerlin cmPerlin;
    private float shakeTimer = 0.0f;
    private float shakeTimerTotal;
    private float startingIntensity = 0.0f;
    [Range(1.0f, 5.0f)] public float intensityMultiplier = 0.75f;
    [Range(1.0f, 5.0f)] public float timeMultiplier = 1.0f;

    private void Awake()
    {
        Instance = this;
        cmVM = GetComponent<CinemachineVirtualCamera>();
        cmPerlin = cmVM.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        cmPerlin.m_AmplitudeGain = intensity * intensityMultiplier;
        startingIntensity = intensity * intensityMultiplier;

        shakeTimerTotal = time * timeMultiplier;
        shakeTimer = time * timeMultiplier;
    }

    private void Update()
    {
        if (shakeTimer > 0) {
            shakeTimer -= Time.deltaTime;
            cmPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1 - (shakeTimer / shakeTimerTotal));
        }
    }
}