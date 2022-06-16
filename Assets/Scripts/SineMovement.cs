using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineMovement : MonoBehaviour
{
    Vector2 startScale;
    float endScale;

    public float scaleFrequency;
    public float scaleAmplitude;

    void Start()
    {
        startScale = transform.localScale;
    }

    void Update()
    {
        endScale = startScale.x + Mathf.Abs(Mathf.Sin(Time.time * scaleFrequency) * scaleAmplitude);
        transform.localScale = new Vector2(endScale, endScale);
    }
}
